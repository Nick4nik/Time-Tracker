using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.Other;
using Time_Tracker.ViewModels.Users;

namespace Time_Tracker.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login", true);
            }

            var user = await GetUserAsync();

            UserInfoViewModel model = new UserInfoViewModel
            {
                user = user,
                IsTasks = false,
                IsTime = false
            };

            if (user.Tasks.Any())
            {
                model.IsTasks = true;
                model.ActiveTasks = user.Tasks.Where(x => !x.IsCompleted).Count();
            }
            if (user.Time.Any())
            {
                model.IsTime = true;
                model.ScheduleHours = user.Post.ScheduleHours;
                model.ScheduleSeconds = user.Post.ScheduleSeconds;
                model.Start = user.Time.Last().Start;
                model.ExpEnd = user.Time.Last().ExpEnd;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login", true);
            }
            var user = await GetUserAsync();
            UserEditViewModel model = new UserEditViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Error = false
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            User user = new User
            {
                Id = model.Id,
                Email = model.Email,
                UserName = model.Name,
            };
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                model.Error = true;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Tasks()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login", true);
            }
            return View();
        }

        [NonAction]
        public async Task<User> GetUserAsync()
        {
            var myId = HttpContext.GetUserIdString();
            return await userManager.FindByIdAsync(myId);
        }
    }
}