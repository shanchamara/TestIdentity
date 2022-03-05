using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestIdentity.BL;
using TestIdentity.Models;

namespace TestIdentity.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly RoleManager<IdentityRole> roleManager;



        public AccountController(IAuthenticationSchemeProvider authprovider, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager)
        {
            _authenticationSchemeProvider = authprovider;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.roleManager = roleManager;
        }
        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            var getUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new LoginModel()
            {
                ExternalLogins = (await _authenticationSchemeProvider.GetAllSchemesAsync())
                 .Select(s => s.DisplayName).Where(p => !string.IsNullOrEmpty(p)),
                ReturnUrl1 = ReturnUrl,
            };
            return View(model);

        }

        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            bool RememberMe = true;
            if (ModelState.IsValid)
            {
                try
                {
                    var dbUser = new UserBl().GetUserByEmail(model.Email);
                    if (dbUser == null)
                        throw new Exception("Invalid Email, please try again");

                    var signinresult = await _signInManager.PasswordSignInAsync(dbUser, model.Password, RememberMe, lockoutOnFailure: false);
                    if (signinresult.Succeeded)
                    {

                        var roles = await _userManager.GetRolesAsync(dbUser);

                        foreach (var item in roles)
                        {

                            return RedirectToAction("Index", "Home");


                        }
                    }
                    else if (signinresult.IsLockedOut)
                    {
                        return View("Lockout");
                    }
                    else if (signinresult.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                    }
                    throw new Exception("Invalid username or password");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            model.ExternalLogins = (await _authenticationSchemeProvider.GetAllSchemesAsync())
            .Select(s => s.DisplayName).Where(p => !string.IsNullOrEmpty(p));
            return View(model);
        }
    }
}
