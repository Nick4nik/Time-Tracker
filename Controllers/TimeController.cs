using Microsoft.AspNetCore.Mvc;

namespace Time_Tracker.Controllers
{
    public class TimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
