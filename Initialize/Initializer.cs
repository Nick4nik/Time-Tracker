using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Time_Tracker.Models;

namespace Time_Tracker.Initialize
{
    public class Initializer
    {
        public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext db)
        {
            #region Initialize list
            string adminRole = "admin";
            string adminEmail = "admin@gmail.com";
            string adminPassword = "Q1w2e3";
            string userRole = "employee";
            string userEmail = "user@gmail.com";
            string userPassword = "Q1w2e3";
            string companyName = "Google";
            string postName1 = "Engineer";
            double postSalary1 = 800;
            double postSchedule1 = 8;
            string postName2 = "Manager";
            double postSalary2 = 1200;
            double postSchedule2 = 12;
            User user = new User
            {
                Email = userEmail,
                UserName = userEmail,
            };
            User admin = new User
            {
                Email = adminEmail,
                UserName = adminEmail
            };
            Company company = new Company
            {
                Name = companyName
            };
            Post post1 = new Post
            {
                Name = postName1,
                Salary = postSalary1,
                Schedule = postSchedule1
            };
            Post post2 = new Post
            {
                Name = postName2,
                Salary = postSalary2,
                Schedule = postSchedule2,
            };
            #endregion

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }
            if (!await db.Companies.AnyAsync())
            {
                db.Companies.Add(company);
                db.SaveChanges();
            }
            if (!await db.Posts.AnyAsync())
            {
                db.Posts.Add(post1);
                db.Posts.Add(post2);
                db.SaveChanges();
                post1.Companies.Add(company);
                post2.Companies.Add(company);
                await db.SaveChangesAsync();
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, adminRole);
                }
            }
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                IdentityResult result = await userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, userRole);
                }
            }
        }
    }
}
