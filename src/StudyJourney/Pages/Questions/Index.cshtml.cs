using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Questions
{
    public class IndexModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public IndexModel(StudyJourneyDbContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get; set; }

        public async Task OnGetAsync()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<Question> questionsQuery = from q in _context.Question
                                                  join t in _context.Topic
                                                  on q.TopicID equals t.TopicID
                                                  where t.UserId == userId
                                                  select q;

            Question = await questionsQuery
                .Include(q => q.Topic)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
