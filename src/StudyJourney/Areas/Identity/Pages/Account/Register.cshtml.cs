using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NToastNotify;
using StudyJourney.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJourney.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationSettings _appsettings;
        private readonly IToastNotification _toastNotification;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                              ILogger<RegisterModel> logger, IOptions<ApplicationSettings> appsettings, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _appsettings = appsettings.Value;
            _toastNotification = toastNotification;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "LastName")]
            public string LastName { get; set; }
            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName };
                if (_appsettings.AutoConfirmEmail)
                {
                    user.EmailConfirmed = true;
                }
                IdentityResult result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("User created a new account with password.");
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, Enums.Roles.Basic.ToString());
                    if (_appsettings.AutoConfirmEmail)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _toastNotification.AddSuccessToastMessage($"Welcome {user.UserName}!");
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        string callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                    }
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
