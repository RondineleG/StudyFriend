using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StudyJourney.Data;
using StudyJourney.Models;
using System;

namespace StudyJourney
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    StudyJourneyDbContext studyFriendContext = services.GetRequiredService<StudyJourneyDbContext>();
                    await DbInitializer.SeedDataAsync(studyFriendContext, userManager);
                    await DbInitializer.SeedRolesAsync(userManager, roleManager);
                    await DbInitializer.SeedSuperAdminAsync(userManager, roleManager);
                    await DbInitializer.SeedBasicUserAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    ILogger<Program> createLoggerFactory = loggerFactory.CreateLogger<Program>();
                    createLoggerFactory.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
