using Microsoft.AspNetCore.Mvc;

namespace Time_Tracker.Controllers
{
    public class TimeController : Controller
    {
        public IActionResult Index()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            return View();
        }
    }
}
