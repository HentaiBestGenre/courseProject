using courseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Controllers
{
    [Authorize]
    public class AccountController: Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public AccountController(UserManager<User> userMgr, SignInManager<User> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(details.Email);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded) { return Redirect("~/Home/Index"); }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
