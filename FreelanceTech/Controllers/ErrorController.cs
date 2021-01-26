using FreelanceTech.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            if (RegisterModel.registeredUser == "FREELANCER")
            {
                return RedirectToAction("Index", "Freelancer");
            }
            else if (RegisterModel.registeredUser == "CUSTOMER")
            {
                return RedirectToAction("Index", "Customer");
            }
            return RedirectToAction("Index","Home");
        }

    }
}
