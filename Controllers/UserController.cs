using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.ViewModels;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                bool message = true;
                return RedirectToAction("Login", "Login", message);
            }
            UserInfoViewModel model = new UserInfoViewModel
            {
                user = await GetUserAsync()
            };

            if (model.user.Tasks.Any())
            {
                model.Tasks = true;
            }
            else
            {
                model.Tasks = false;
            }
            if (model.user.Time.Any())
            {
                model.Time = true;
            }
            else
            {
                model.Time = false;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            if (!User.Identity.IsAuthenticated)
            {
                bool message = true;
                return RedirectToAction("Login", "Login", message);
            }

            UserEditViewModel model = new UserEditViewModel
            {
                user = await GetUserAsync(),
                Message = true
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Message = false;
                return View(model);
            }

            User user = model.user;
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                model.Message = false;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Tasks()
        {
            if (!User.Identity.IsAuthenticated)
            {
                bool message = true;
                return RedirectToAction("Login", "Login", message);
            }
            return View();
        }

        [NonAction]
        public async Task<User> GetUserAsync()
        {
            var myId = HttpContext.GetUserId();
            return await userManager.FindByIdAsync(myId);
        }
    }
}
