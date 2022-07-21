using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.ViewModels.Logins;

namespace Time_Tracker.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationContext db;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Login(bool error = false)
        {
            LoginRegisterViewModel model = new LoginRegisterViewModel();
            if (error)
            {
                model.Error = true;
                await GetLists(model);
                return View(model);
            }
            model.Error = false;
            await GetLists(model);
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
                model.Error = true;
                await GetLists(model);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterViewModel model)
        {
            try
            {
                User user = new User
                {
                    Email = model.RegisterEmail,
                    UserName = model.RegisterEmail,
                };
                var company = await db.Companies.FindAsync(model.CompanyId);
                var post = await db.Posts.FindAsync(model.PostId);
                var result = await userManager.CreateAsync(user, model.RegisterPassword);

                if (!result.Succeeded)
                {
                    model.Error = true;
                    await GetLists(model);
                    return View("Login", model);
                }

                company.Users.Add(user);
                post.Users.Add(user);

                await db.SaveChangesAsync();
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                model.Error = true;
                await GetLists(model);
                return View("Login", model);
            }
            
        }

        [NonAction]
        public async Task<LoginRegisterViewModel> GetLists(LoginRegisterViewModel model)
        {
            model.RegisterCompany = await db.Companies.ToListAsync();
            model.RegisterPost = await db.Posts.ToListAsync();
            return model;
        }
    }
}
