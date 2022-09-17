using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Answers
{
    public class IndexModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public IndexModel(StudyJourneyDbContext context)
        {
            _context = context;
        }

        public IList<Answer> Answer { get; set; }

        public async Task OnGetAsync()
        {
            Answer = await _context.Answer
                .Include(a => a.Question).ToListAsync();
        }
    }
}
