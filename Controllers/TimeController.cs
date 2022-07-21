using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.Other;
using Time_Tracker.ViewModels.Times;

namespace Time_Tracker.Controllers
{
    public class TimeController : Controller
    {
        private readonly Tracker tracker;
        private readonly ApplicationContext db;
        private readonly UserManager<User> userManager;

        public TimeController(Tracker tracker, ApplicationContext db, UserManager<User> userManager)
        {
            this.tracker = tracker;
            this.userManager = userManager;
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string message = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login", true);
            }
            TimeTodayViewModel model = await GetTodayAsync();
            if (message != "")
            {
                model.Message = message;
            }
            if (model == null)
            {
                TimeTodayViewModel modelError = new TimeTodayViewModel();
                modelError.Message = "No entries yet";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login", true);
            }
            TimeHistoryViewModel model = await GetHistoryAsync();
            if (model == null)
            {
                return RedirectToAction("Index", "Time", "No entries yet");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Start()
        {
            var user = await GetUserAsync();
            try
            {
                var result = await tracker.StartAsync(user);
                if (result.Error != null)
                {
                    return RedirectToAction("Index", "Time", result.Error);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Oops. Something went wrong. Please repeat");
            }

            return RedirectToAction("Index", "Time", //"IsStart");
        }

        [HttpPost]
        public async Task<IActionResult> Pause()
        {
            var user = await GetUserAsync();
            try
            {
                var result = await tracker.PauseAsync(user);
                if (result.Error != null)
                {
                    return RedirectToAction("Index", "Time", result.Error);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Oops. Something went wrong. Please repeat");
            }
            
            return RedirectToAction("Index", "Time", //"IsPaused");
        }

        [HttpPost]
        public async Task<IActionResult> Finish()
        {
            var user = await GetUserAsync();
            try
            {
                var result = await tracker.FinishAsync(user);
                if (result.Error != null)
                {
                    return RedirectToAction("Index", "Time", result.Error);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Oops. Something went wrong. Please repeat");
            }
            return RedirectToAction("Index", "Time", //"IsEnd");
        }

        [NonAction]
        public async Task<TimeHistoryViewModel> GetHistoryAsync()
        {
            TimeHistoryViewModel model = new TimeHistoryViewModel();
            User user = await GetUserAsync();
            var q = await db.Times.Where(x=> x.User == user).ToListAsync();
            if (q == null)
            {
                model.IsExist = false;
                return null;
            }
            model.Time = q;
            return model;
        }

        [NonAction]
        public async Task<TimeTodayViewModel> GetTodayAsync()
        {
            TimeTodayViewModel model = new TimeTodayViewModel();
            User user = await GetUserAsync();
            var now = Convert.ToString(DateTime.Today)[..^8];
            var q = await db.Times.Where(x => x.User == user).Where(x => x.Date == now).ToListAsync();
            if (q == null)
            {
                model.IsExist = false;
                return null;
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