using StudyJourney.Data;
using StudyJourney.Models;
using System.Linq;
using Xunit;

namespace StudyJourneyTests
{
    public class DataTests
    {
        [Fact]
        public void FindTopic_ByUserId()
        {
            using (var db = new StudyJourneyDbContext(Utilities.TestDbContextOptions()))
            {
                string user_id = "test_id";
                var topics = new Topic[]
                {
                    new Topic { Name = "JavaScript", UserId = user_id}
                };
                foreach (Topic t in topics)
                {
                    db.Topic.Add(t);
                }
                db.SaveChanges();

                var topic = (from t in db.Topic
                             where t.UserId == user_id
                             select t).Single();

                Assert.Equal(topic.UserId, user_id);
            }
        }
    }
}
