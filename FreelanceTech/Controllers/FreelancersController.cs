using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreelanceTech.Data;
using FreelanceTech.Models;
using FreelanceTech.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Shared.Web.MvcExtensions;
using FreelanceTech.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using YenePaySdk;
using System.Diagnostics;

namespace FreelanceTech.Controllers
{
    public class FreelancersController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private string currentUser;

        private readonly IProposalRepository proposalRepository;
        private readonly IWalletRepository walletRepository;


        public FreelancersController(ApplicationDbContext context = null, IWebHostEnvironment hostingEnvironment = null, IProposalRepository proposalRepository = null, IWalletRepository walletRepository = null)
        {
            currentUser = RegisterModel.registeredUser;
            this.proposalRepository = proposalRepository;
            this.walletRepository = walletRepository;
            _context = context;
            _hostingEnvironment = hostingEnvironment;


        }
        [HttpGet]
        public IActionResult SubmitProposal()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitProposal(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                string id = rnd.Next().ToString();
                proposal.proposalId = id;
                proposal.freelancerId = "1";
                proposal.jobId = "1";
                proposalRepository.SubmitProposal(proposal);
                return View();
            }
            return View();
        }

        public IActionResult Register()
        {
          
            return View();
        }
        // GET: Freelancers

        public async Task<IActionResult> Index()
        {

          var freelancer = await _context.Freelancer
                 .FirstOrDefaultAsync(m => m.freelancerId == currentUser);
            var address = await _context.Address
                 .FirstOrDefaultAsync(m => m.userId == currentUser);
            var expertise = await _context.Expertise
                 .FirstOrDefaultAsync(m => m.freelancerId == currentUser);
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == currentUser);
            //Job  var  = await _context.Freelancer
            //                 .FirstOrDefaultAsync(m => m.freelancerId == currentUser);
            FreelancerViewModel model = new FreelancerViewModel();
           
            model.freelancerId = freelancer.freelancerId;
            model.phoneNumber = freelancer.phoneNumber;
            model.title = freelancer.title;
            model.rate = freelancer.rate;
            model.score = freelancer.score;
            model.lastName = user.lastName;
            model.firstName = user.firstName;
           /* model.expertiseStatus = expertise.status;*//*
            model.professionalOverview = freelancer.professionalOverview;
            model.hourlyRate = freelancer.hourlyRate;
            *//*model.photo = freelancer.photo;*//*

            model.houseNumber = address.houseNumber;*/
            return View();
        }

        // GET: Freelancers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freelancer = await _context.Freelancer
                .FirstOrDefaultAsync(m => m.freelancerId == id);
            if (freelancer == null)
            {
                return NotFound();
            }

            return View(freelancer);
        }

      

        // POST: Freelancers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public async Task<IActionResult> Register(CreateFreelancerViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(viewmodel);
                Freelancer freelancer = new Freelancer();

                freelancer.freelancerId = currentUser;

                freelancer.englishProficiency = (int)viewmodel.englishProficiency;
                string userId = User.GetUserId();
                freelancer.hourlyRate = Convert.ToDouble(viewmodel.hourlyRate);
                freelancer.photo = uniqueFileName;
                freelancer.title = viewmodel.title;
                freelancer.status = (int)Constants.status.Active;
                freelancer.phoneNumber = viewmodel.phoneNumber;
                freelancer.professionalOverview = viewmodel.professionalOverview;

                Language lang = new Language();
                lang.userId = currentUser;
                lang.language = (int)viewmodel.language;

                Address address = new Address();
                address.region = viewmodel.region;
                address.city = viewmodel.city;
                address.woreda = viewmodel.woreda;
                address.houseNumber = Convert.ToInt32(viewmodel.houseNumber.ToString());
                address.pobox = Convert.ToInt32(viewmodel.pobox.ToString());
                address.userId = currentUser;

                Expertise expertise = new Expertise();
                expertise.skill = (int)viewmodel.skill;
                expertise.category = (int)viewmodel.category;
                expertise.level = viewmodel.expertiseStatus;
                expertise.status = "not verified";

                Experience experience = new Experience();
                experience.companyName = viewmodel.companyName;
                experience.title = viewmodel.expTitle;
                experience.description = viewmodel.description;
                experience.freelancerId = currentUser;
                experience.location = viewmodel.location;
                experience.startDate = viewmodel.startDate;
                experience.endDate = viewmodel.endDate;

                _context.Add(freelancer);
                await _context.SaveChangesAsync();
                _context.Add(address);
                await _context.SaveChangesAsync();

                _context.Add(expertise);
                await _context.SaveChangesAsync();

                _context.Add(experience);
                await _context.SaveChangesAsync();


                _context.Add(lang);
                await _context.SaveChangesAsync();
             


                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }

        private string ProcessUploadedFile(CreateFreelancerViewModel model)
        {
            string uniqueFileName = null;
            if (model.photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        // GET: Freelancers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freelancer = await _context.Freelancer.FindAsync(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            return View(freelancer);
        }

        public async Task<IActionResult> requestSkillVerification(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freelancer = await _context.Freelancer.FindAsync(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("freelancerId,phoneNumber,education,id,englishProficiency,hourlyRate,title,professionalOverview,photo,legaID,rate,score,status")] Freelancer freelancer)
        {
            if (id != freelancer.freelancerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(freelancer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FreelancerExists(freelancer.freelancerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(freelancer);
        }

        // GET: Freelancers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freelancer = await _context.Freelancer
                .FirstOrDefaultAsync(m => m.freelancerId == id);
            if (freelancer == null)
            {
                return NotFound();
            }

            return View(freelancer);
        }

        // POST: Freelancers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var freelancer = await _context.Freelancer.FindAsync(id);
            _context.Freelancer.Remove(freelancer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FreelancerExists(string id)
        {
            return _context.Freelancer.Any(e => e.freelancerId == id);
        }




    }
}
