using courseProject.Models;
using courseProject.Services;
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
        private ApplicationDbContext db;
        private ICollectionInterface CollectionInterface;
        public AccountController(UserManager<User> userMgr, SignInManager<User> signInMgr,
            ApplicationDbContext _db, ICollectionInterface ici)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            db = _db;
            CollectionInterface = ici;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(details.Email);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded) {
                        CookieOptions cookieOptions = new CookieOptions();
                        cookieOptions.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Append("userId", user.Id, cookieOptions);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        [AllowAnonymous]
        public IActionResult Registration()
        {
            if (HttpContext.User.Identity.IsAuthenticated) 
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(CreateUserModel model)
        {
            if (ModelState.IsValid && model.Password == model.RepitePassword)
            {
                User user = new User { UserName = model.UserName, Email = model.Email };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) { 
                    ProfilePage profilePage = new ProfilePage()
                    {
                        Description = "Discription",
                        Online = DateTime.Now,
                        User = user,
                    };
                    db.ProfilePages.Add(profilePage);
                    db.SaveChanges();
                    return RedirectToAction("Login"); }
                else { AddErrorsFromResult(result); }
            }
            ModelState.AddModelError("", "Wrong values");
            return View(model); // отображать ошибку
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            Response.Cookies.Delete("userId");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Profile(string userId)
        {
            return View(
                new ProfileViewModel()
                {
                    ProfilePage = db.ProfilePages.Where(c => c.User.Id == userId).First(),
                    User = db.Users.Where(c => c.Id == userId).First(),
                    Collections = CollectionInterface.Collections(userId)
                }
                );
        }
        
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
