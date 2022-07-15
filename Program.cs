using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Time_Tracker.Initialize;
using Time_Tracker.Models;

namespace Time_Tracker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            /// Initializer
            ///
            ///using (var scope = host.Services.CreateScope())
            ///{
            ///    var services = scope.ServiceProvider;
            ///    try
            ///    {
            ///        UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
            ///        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            ///        var db = new ApplicationContext();
            ///        await Initializer.Initialize(userManager, rolesManager, db);
            ///    }
            ///    catch (Exception ex)
            ///    {
            ///        var logger = services.GetRequiredService<ILogger<Program>>();
            ///        logger.LogError(ex, "An error occurred while seeding the database.");
            ///    }
            ///}
            ///
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
