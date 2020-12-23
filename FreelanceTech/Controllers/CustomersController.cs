using FreelanceTech.Areas.Identity.Pages.Account;
using FreelanceTech.Data;
using FreelanceTech.Models;
using FreelanceTech.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Web.MvcExtensions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace FreelanceTech.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        string currentUser = RegisterModel.registeredUser;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CustomersController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Register()
        {

            return View();
        }
      
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Register(CreateCustomerViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(viewmodel);
                Customer customer = new Customer();


                customer.customerId = currentUser;

                customer.englishProficiency = (int)viewmodel.englishProficiency;
                string userId = User.GetUserId();             
                customer.photo = uniqueFileName;
                customer.status = (int)Constants.status.Active;
                customer.phoneNumber = viewmodel.phoneNumber;

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


                _context.Add(customer);
                await _context.SaveChangesAsync();
                _context.Add(address);
                await _context.SaveChangesAsync();

                _context.Add(lang);
                await _context.SaveChangesAsync();
           


                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }

        private string ProcessUploadedFile(CreateCustomerViewModel model)
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


        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("customerId,photo,legalId,status,language,phoneNumber,englishProficiency")] Customer customer)
        {
            if (id != customer.customerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.customerId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.customerId == id);
        }
    }
}
