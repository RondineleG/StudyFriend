using Microsoft.AspNetCore.Identity;
using StudyFriend.Models;
using System.Linq;

namespace StudyFriend.Data
{
    public static class DbInitializer

    {
        public static async System.Threading.Tasks.Task InitializeAsync(
            StudyFriendContext context,
            UserManager<ApplicationUser> userManager
            )
        {

            context.Database.EnsureCreated();

            // Don't seed db if test ApplicationUser exists
            if (context.ApplicationUser.Any())
            {
                return;
            }

            var user = new ApplicationUser { UserName = "test@test.com", Email = "test@test.com" };
            _ = await userManager.CreateAsync(user, "P@ssw0rd1!");
            context.SaveChanges();

            var topics = new Topic[]
            {
                new Topic { Name = "JavaScript", UserId = user.Id}
            };
            foreach (Topic t in topics)
            {
                context.Topic.Add(t);
            }
            context.SaveChanges();


            var questions = new Question[]
            {
                new Question{ Body="How do you stop event bubbling?", TopicID=topics[0].TopicID }
            };
            foreach (Question q in questions)
            {
                context.Question.Add(q);
            }
            context.SaveChanges();

            var answers = new Answer[]
            {
                new Answer { Body="event.stopPropagation()", QuestionID = questions[0].QuestionID }
            };
            foreach (Answer a in answers)
            {
                context.Answer.Add(a);
            }
            context.SaveChanges();
        }
    }
}
