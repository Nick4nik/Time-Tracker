using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Time_Tracker.Models;

namespace Time_Tracker.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext db;

        public UserController(UserManager<User> userManager, ApplicationContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var myId = HttpContext.GetUserId();

            return View();
        }
    }
}
