using Microsoft.AspNetCore.Mvc;

namespace Time_Tracker.Controllers
{
    public class TimeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                bool message = true;
                return RedirectToAction("Login", "Login", message);
            }
            return View();
        }
    }
}
