using courseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace courseProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleMng, UserManager<User> userMng) 
        {
            roleManager = roleMng;
            userManager = userMng;
        }

        [Route("/RoleAdmin/")]
        public ViewResult Index() => View(roleManager.Roles);
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded) { return RedirectToAction("Index"); }
                else { AddErrorsFromResult(result); }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) { return RedirectToAction("Index"); }
                else { AddErrorsFromResult(result); }
            }
            else { ModelState.AddModelError("", "No role found"); }
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Edit(string id, string actionName)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<User> members = new List<User>();
            List<User> nonMembers = new List<User>();
            foreach(User user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers; /////// Error
                list.Add(user);
            }
            return View(new RoleEditModel {
                ActionName = actionName,
                Role = role,
                Members = actionName == "AddMembers" ? nonMembers : members
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddMembers(RoleModeficationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.Ids ?? new string[] { })
                {
                    User user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded) { AddErrorsFromResult(result); }
                    }
                }
            }
            if (ModelState.IsValid) { return RedirectToAction("Index"); }
            else { return await Edit(model.RoleId, model.ActionName); }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMembers(RoleModeficationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.Ids ?? new string[] { })
                {
                    User user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded) { AddErrorsFromResult(result); }
                    }
                }
            }
            if (ModelState.IsValid) { return RedirectToAction("Index"); }
            else { return await Edit(model.RoleId, model.ActionName); }
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
