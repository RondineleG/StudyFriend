using Microsoft.AspNetCore.Identity;
using StudyJourney.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudyJourney.Data
{
    public static class DbInitializer

    {
        public static async Task SeedDataAsync(StudyJourneyDbContext context, UserManager<ApplicationUser> userManager)
        {

            context.Database.EnsureCreated();

            // Don't seed db if test ApplicationUser exists
            if (context.ApplicationUser.Any()) return;


            ApplicationUser user = new ApplicationUser { UserName = "test@test.com", Email = "test@test.com" };
            _ = await userManager.CreateAsync(user, "P@ssw0rd1!");
            context.SaveChanges();

            Topic[] topics = new Topic[]
            {
                new Topic { Name = "JavaScript", UserId = user.Id}
            };
            foreach (Topic t in topics)
            {
                context.Topic.Add(t);
            }
            context.SaveChanges();


            Question[] questions = new Question[]
            {
                new Question{ Body="How do you stop event bubbling?", TopicID=topics[0].TopicID }
            };
            foreach (Question q in questions)
            {
                context.Question.Add(q);
            }
            context.SaveChanges();

            Answer[] answers = new Answer[]
            {
                new Answer { Body="event.stopPropagation()", QuestionID = questions[0].QuestionID }
            };
            foreach (Answer a in answers)
            {
                context.Answer.Add(a);
            }
            context.SaveChanges();
        }

        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            ApplicationUser defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Rondinele",
                LastName = "Guimaraes",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                ApplicationUser user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new ApplicationUser
            {
                UserName = "basic",
                Email = "basic@gmail.com",
                FirstName = "Beatriz",
                LastName = "Guimaraes",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                ApplicationUser user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                }

            }
        }
    }
}
