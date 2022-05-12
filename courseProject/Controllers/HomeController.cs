using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using courseProject.Services;
using courseProject.Models;
using Microsoft.AspNetCore.Identity;

namespace courseProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHomePageInterface HomePageInterface;

        public HomeController(ILogger<HomeController> logger, IHomePageInterface hpi) { 
            _logger = logger;
            HomePageInterface = hpi;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, string id = "Item")
        {
            /* Initialize DB*/
            //SeedData t = new SeedData(db, UserManager, RoleManager);
            //await t.Adding();
            ViewBag.SearchString = searchString;
            return View(HomePageInterface.Collections(id, searchString));
        }

    }
}