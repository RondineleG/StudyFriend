using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using System.Linq;
using System.Security.Claims;

namespace StudyJourney.Pages.Questions
{
    public class TopicNamePageModel : PageModel
    {
        public SelectList TopicNameSL { get; set; }

        public void PopulateTopicsDropDownList(StudyJourneyDbContext _context,
            object selectedTopic = null)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IOrderedQueryable<Models.Topic> topicsQuery = from t in _context.Topic
                                                          where t.UserId == userId
                                                          orderby t.Name
                                                          select t;
            TopicNameSL = new SelectList(topicsQuery.AsNoTracking(),
                "TopicID", "Name", selectedTopic);
        }
    }
}
