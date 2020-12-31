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
using System.Collections.Generic;


namespace FreelanceTech.Controllers
{
    public class AdminController : Controller
       {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            string currentUser = RegisterModel.registeredUser;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index ()
        {
             IList<User> user = _context.Users.ToList();
             IList<Freelancer> freelancer = _context.Freelancer.ToList();
            IList<Customer> customers = _context.Customer.ToList();
            IList<Job> jobs = _context.Job.ToList();
           
            AdminViewModel model = new AdminViewModel();
            
            model.TotalUser=user.Count();
            model.TotalFreelance = freelancer.Count();
            model.TotalCustomer= customers.Count();
            model.TotalJob = jobs.Count();
            model.TotalJobInProgress = jobs.Where(x => x.status == "On Progress").Count();
            string userId = User.GetUserId();
            var Admin = await _context.Users
             .FirstOrDefaultAsync(m => m.Id == userId);
            model.lastName = Admin.lastName;
            model.firstName = Admin.firstName;
            
            return View(model);
        }
    }
}
