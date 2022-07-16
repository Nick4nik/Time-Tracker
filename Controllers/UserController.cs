using Microsoft.AspNetCore.Mvc;

namespace Time_Tracker.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
