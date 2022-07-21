using Microsoft.AspNetCore.Mvc;

namespace Time_Tracker.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                bool error = true;
                return RedirectToAction("Login", "Login", error);
            }
            return View();
        }
    }
}
