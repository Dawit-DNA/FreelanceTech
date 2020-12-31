using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FreelanceTech.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FreelanceTech.Areas.Identity.Pages.Account;
using Shared.Web.MvcExtensions;
using FreelanceTech.Data;
using Microsoft.EntityFrameworkCore;

namespace FreelanceTech.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public const string BusinessAnalystRoleId = "b43d8cac-508c-4879-ace3-31ee0bda1e3c";
        public const string CustomerRoleId = "fa32a1e3-5354-4fc7-a775-8bbeeddbb307";
        public const string FreelancerRoleId = "daad3bf9-fc6e-4b20-a8a7-8e011ed592c6";
        public const string AdministratorRoleId = "b51cf2a2-9681-4e8d-8e73-dee7e8966cd5";
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;

        public LoginModel(SignInManager<User> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<User> userManager, ApplicationDbContext context = null)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    RegisterModel.registeredUser = User.GetUserId();
                    var user = await _context.UserRoles
                    .FirstOrDefaultAsync(m => m.UserId == RegisterModel.registeredUser);
                    switch (user.RoleId){
                        case CustomerRoleId:
                            {
                                returnUrl = returnUrl ?? Url.Content("~/Customers/Index");
                                break;
                            }
                        case FreelancerRoleId:
                            {
                                returnUrl = returnUrl ?? Url.Content("~/Freelancers/Index");
                                break;
                            }
                        case AdministratorRoleId:
                            {
                                returnUrl = returnUrl ?? Url.Content("~/Administrators/Index");
                                break;
                            }
                        case BusinessAnalystRoleId:
                            {
                                returnUrl = returnUrl ?? Url.Content("~/BusinessAnalyst/Index");
                                break;
                            }
                        default:
                            {
                                returnUrl = returnUrl ?? Url.Content("~/Home/Index");
                                break;
                            }
                    }
                        returnUrl = returnUrl ?? Url.Content("~/Freelancers/Index");
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
