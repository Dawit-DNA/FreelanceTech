using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FreelanceTech.Models;
using Shared.Web.MvcExtensions;
using FreelanceTech.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FreelanceTech.Areas.Identity.Pages.Account;

namespace FreelanceTech.Controllers
{
    public class HomeController : Controller
    {
        string currentUser = RegisterModel.registeredUser;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager,
                                        UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();

        }
        public IActionResult account()
        {
            return Content(this.User.GetUserId());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChooseAccount()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChooseAccount(ChooseAccountViewModel model)
        {
            
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(currentUser);
            
               
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {currentUser} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    IEnumerable<string> Role = new string[]{ model.userRole };
                  
                    user.firstName = model.firstName;
                    user.lastName = model.lastName;
                    var result = await userManager.AddToRolesAsync(user, Role);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Cannot add selected roles to user");
                        return View(model);
                    }
                    if(model.userRole == "Freelancer")
                    return RedirectToAction("Register", "Freelancers");
                    return RedirectToAction("Register", "Customers");
                }

            }
            
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
