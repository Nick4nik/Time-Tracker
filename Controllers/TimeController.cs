using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.Other;
using Time_Tracker.ViewModels.Time;
using Time_Tracker.ViewModels.Times;

namespace Time_Tracker.Controllers
{
    public class TimeController : Controller
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User> userManager;

        public TimeController(ApplicationContext db, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                bool message = true;
                return RedirectToAction("Login", "Login", message);
            }
            TimeTodayViewModel model = new TimeTodayViewModel();
            model = await GetTodayAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            if (!User.Identity.IsAuthenticated)
            {
                bool message = true;
                return RedirectToAction("Login", "Login", message);
            }
            TimeHistoryViewModel model = new TimeHistoryViewModel();
            model = await GetHistoryAsync(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Start()
        {
            User user = await GetUserAsync();
            var now = Convert.ToString(DateTime.Today);
            now = now[..^9];
            var q = db.Times.Where(x => x.User == user).Where(x => x.Date == now).ToListAsync();
        }


        [NonAction]
        public async Task<TimeHistoryViewModel> GetHistoryAsync(TimeHistoryViewModel model)
        {
            User user = await GetUserAsync();
            var q = await db.Times.Where(x=> x.User == user).ToListAsync();
            if (q == null)
            {
                model.IsExist = false;
                return model;
            }
            model.Time = q;
            return model;
        }

        [NonAction]
        public async Task<TimeTodayViewModel> GetTodayAsync(TimeTodayViewModel model)
        {
            User user = await GetUserAsync();
            var now = Convert.ToString(DateTime.Today);
            now = now[..^9];
            var q = await db.Times.Where(x => x.User == user).Where(x => x.Date == now).ToListAsync();
            if (q != null)
            {
                model.IsExist = false;
            }
            model.Time = q;
            return model;
        }

        [NonAction]
        public async Task<User> GetUserAsync()
        {
            var myId = HttpContext.GetUserIdString();
            return await userManager.FindByIdAsync(myId);
        }
    }
}