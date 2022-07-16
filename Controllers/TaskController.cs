using Microsoft.AspNetCore.Mvc;

namespace Time_Tracker.Controllers
{
    public class TaskController : Controller
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
