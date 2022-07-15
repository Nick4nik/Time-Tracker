using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.ViewModels;

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
        public async Task<IActionResult> Login()
        {
            LoginRegisterViewModel model = new LoginRegisterViewModel();
            model.Message = false;
            await Lists(model);
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
                Lists(model);
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
                
            };
            var company = await db.Companies.FindAsync(model.CompanyId);
            var post = await db.Posts.FindAsync(model.PostId);
            var result = await userManager.CreateAsync(user, model.RegisterPassword);

            if (!result.Succeeded)
            {
                model.Message = true;
                model.RegisterCompany = await db.Companies.ToListAsync();
                model.RegisterPost = await db.Posts.ToListAsync();
                return View("Login", model);
            }

            user.Company.Add(company);
            user.Post.Add(post);

            await db.SaveChangesAsync();
            await signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<LoginRegisterViewModel> Lists(LoginRegisterViewModel model)
        {
            model.RegisterCompany = await db.Companies.ToListAsync();
            model.RegisterPost = await db.Posts.ToListAsync();
            return model;
        }
    }
}
