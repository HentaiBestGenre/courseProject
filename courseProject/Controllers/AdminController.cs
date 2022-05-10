using courseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<User> UserManager;
        ApplicationDbContext db;
        public AdminController(UserManager<User> userMgr, ApplicationDbContext _db)
        {
            UserManager = userMgr;
            db = _db;
        }
        [Route("/Admin")]
        public ViewResult Index() => View(UserManager.Users);

        public ViewResult Create() => View(new CreateUserModel());

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid && model.Password == model.RepitePassword)
            {
                User user = new User { UserName = model.UserName, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    ProfilePage profilePage = new ProfilePage()
                    {
                        Description = "Discription",
                        Online = DateTime.Now,
                        User = user,
                    };
                    db.ProfilePages.Add(profilePage);
                    db.SaveChanges();
                    return RedirectToAction("Index"); }
                else { AddErrorsFromResult(result); }
            }
            else { ModelState.AddModelError("", "Wrong mail or password"); }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string[] ids)
        {   foreach (string id in ids)
            {
                User user = await UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await UserManager.DeleteAsync(user);
                    if (!result.Succeeded) { AddErrorsFromResult(result); }
                }
                else { ModelState.AddModelError("", "User not Found"); }
            }
            return View("Index", UserManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
