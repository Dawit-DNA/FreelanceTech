﻿using System;
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
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> SubmitProposal()
        {
            string jobId = Request.Query["jobId"].ToString();
            UserViewModel userViewModel = new UserViewModel();
            string userId = User.GetUserId();
            var freelancer = await _context.Freelancer
                  .FirstOrDefaultAsync(m => m.freelancerId == userId);
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == userId);

            FreelancerViewModel model = new FreelancerViewModel();

            model.freelancerId = freelancer.freelancerId;
            model.phoneNumber = freelancer.phoneNumber;
            model.lastName = user.lastName;
            model.firstName = user.firstName;

            model.photo = freelancer.photo;
            userViewModel.job = new Job();
            userViewModel.job.jobId = jobId;
            userViewModel.FreelancerViewModel = model;
            return View(userViewModel);
        }
        [HttpPost]
        public IActionResult SubmitProposal(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                string id = rnd.Next().ToString();
                proposal.proposalId = id;
                proposal.freelancerId = RegisterModel.registeredUser;
                proposal.jobId = Request.Form["jobId"].ToString();
                proposalRepository.SubmitProposal(proposal);
                return RedirectToAction("Index", "Freelancers");
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
            UserViewModel userViewModel = new UserViewModel();
            string userId = User.GetUserId();
            var freelancer = await _context.Freelancer
                  .FirstOrDefaultAsync(m => m.freelancerId == userId);
            var address = await _context.Address
                 .FirstOrDefaultAsync(m => m.userId == userId);
            var expertise = await _context.Expertise
                 .FirstOrDefaultAsync(m => m.freelancerId == userId);
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == userId);
            var job = _context.Job;
            FreelancerViewModel model = new FreelancerViewModel();
            var proposal = _context.Proposal.Where(m => m.freelancerId == userId);
            List<ProposalViewModel> proposals = new List<ProposalViewModel>();
            ProposalViewModel prop = new ProposalViewModel();
            foreach (var item in proposal)
            {
                prop.jobId = item.jobId;
                prop.description = item.description;
                prop.answers = item.answers;
                prop.bidAmount = item.bidAmount;
                prop.jobTitle = _context.Job.FirstOrDefault(m => m.jobId == item.jobId).title;
                proposals.Add(prop);
            }
            model.freelancerId = freelancer.freelancerId;
            model.phoneNumber = freelancer.phoneNumber;
            model.title = freelancer.title;
            model.rate = freelancer.rate;
            model.score = freelancer.score;
            model.lastName = user.lastName;
            model.firstName = user.firstName;
            model.region = address.region;
            model.city = address.city;
            model.expertiseStatus = "Unverified";
            model.professionalOverview = freelancer.professionalOverview;
            model.hourlyRate = freelancer.hourlyRate;

            model.photo = freelancer.photo;
            model.houseNumber = address.houseNumber;
            userViewModel.FreelancerViewModel = model;
            userViewModel.jobs = job.ToList();
            userViewModel.proposals = proposals;
            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ViewJob()
        {
            string jobId = Request.Form["jobId"].ToString();
            UserViewModel userViewModel = new UserViewModel();
            string userId = User.GetUserId();
            var freelancer = await _context.Freelancer
                  .FirstOrDefaultAsync(m => m.freelancerId == userId);
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == userId);
            var job = _context.Job.FirstOrDefault(m => m.jobId == jobId);
            var customerUser = _context.Users.FirstOrDefault(m => m.Id == job.customerId);
            FreelancerViewModel model = new FreelancerViewModel();
            //var customer = _context.Customer.FirstOrDefault(m => m.customerId == job.customerId);
            model.freelancerId = freelancer.freelancerId;
            model.lastName = user.lastName;
            model.firstName = user.firstName;

            model.photo = freelancer.photo;
            userViewModel.FreelancerViewModel = model;
            userViewModel.customerViewModel = new CustomerViewModel();
            userViewModel.customerViewModel.firstName = customerUser.firstName;
            userViewModel.customerViewModel.lastName = customerUser.lastName;
            userViewModel.customerViewModel.phoneNumber = customerUser.PhoneNumber;
            userViewModel.job = job;
            return View(userViewModel);
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
                Wallet wall = new Wallet();
                wall.userId = RegisterModel.registeredUser;
                wall.balance = 0;
                walletRepository.Deposit(wall);
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



                return RedirectToAction("Index", "Home");
            }
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult OnGoingProjects()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.jobs = new List<Job>();
            userViewModel.FreelancerViewModel = new FreelancerViewModel();
            string freelancerId = RegisterModel.registeredUser;
            var freelancer = _context.Freelancer.FirstOrDefault(m => m.freelancerId == freelancerId);
            var freelancer1 = _context.Users.FirstOrDefault(m => m.Id == freelancerId);
            userViewModel.FreelancerViewModel.freelancerId = freelancer.freelancerId;
            userViewModel.FreelancerViewModel.firstName = freelancer1.firstName;
            userViewModel.FreelancerViewModel.lastName = freelancer1.lastName;
            userViewModel.FreelancerViewModel.photo = freelancer.photo;
            var jobs = _context.Job.Where(m => m.freelancerId == freelancerId);
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            Job jobs1 = new Job();

            foreach (var item in jobs)
            {
                jobs1.jobId = item.jobId;
                jobs1.title = item.title;
                jobs1.Payment_Amount = item.Payment_Amount;
                var user = _context.Users.FirstOrDefault(m => m.Id == item.customerId);
                jobs1.customerViewModel = new CustomerViewModel() { customerId = item.customerId, firstName = user.firstName, lastName = user.lastName };
                userViewModel.jobs.Add(jobs1);
            }
            return View(userViewModel);
        }
        [HttpGet]
        public IActionResult ProjectsWorkedOn()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.jobs = new List<Job>();
            userViewModel.FreelancerViewModel = new FreelancerViewModel();
            string freelancerId = RegisterModel.registeredUser;
            var freelancer = _context.Freelancer.FirstOrDefault(m => m.freelancerId == freelancerId);
            var freelancer1 = _context.Users.FirstOrDefault(m => m.Id == freelancerId);
            userViewModel.FreelancerViewModel.freelancerId = freelancer.freelancerId;
            userViewModel.FreelancerViewModel.firstName = freelancer1.firstName;
            userViewModel.FreelancerViewModel.lastName = freelancer1.lastName;
            userViewModel.FreelancerViewModel.photo = freelancer.photo;
            var jobs = _context.Job.Where(m => m.freelancerId == freelancerId & m.status == "Ended");
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            Job jobs1 = new Job();

            foreach (var item in jobs)
            {
                jobs1.jobId = item.jobId;
                jobs1.title = item.title;
                jobs1.Payment_Amount = item.Payment_Amount;
                var user = _context.Users.FirstOrDefault(m => m.Id == item.customerId);
                jobs1.customerViewModel = new CustomerViewModel() { customerId = item.customerId, firstName = user.firstName, lastName = user.lastName };
                userViewModel.jobs.Add(jobs1);
            }
            return View(userViewModel);
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
