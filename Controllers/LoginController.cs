using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.ViewModels;

namespace Time_Tracker.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRegisterViewModel model = new LoginRegisterViewModel();
            model.Message = false;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRegisterViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(
                model.LoginEmail, model.LoginPassword, model.LoginRememberMe, false);
            if (!result.Succeeded)
            {
                model.Message = true;
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterViewModel model)
        {
            User user = new User
            {
                Email = model.RegisterEmail,
                UserName = model.RegisterEmail,
                Company = model.RegisterCompany,
                Post = model.RegisterPost
            };
            var result = await userManager.CreateAsync(user, model.RegisterPassword);

            if (!result.Succeeded)
            {
                model.Message = true;
                return View("Login");
            }

            await signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
