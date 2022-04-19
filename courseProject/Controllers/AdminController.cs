using courseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> UserManager;
        public AdminController(UserManager<User> userMgr) => UserManager = userMgr;
        
        [Route("/Admin")]
        public ViewResult Index() => View(UserManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState.IsValid");
                User user = new User { UserName = model.UserName, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded) { return RedirectToAction("Index"); }
                else { AddErrorsFromResult(result); }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded) { return RedirectToAction("Index"); }
                else { AddErrorsFromResult(result); }
            }
            else { ModelState.AddModelError("", "User not Found"); }
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
