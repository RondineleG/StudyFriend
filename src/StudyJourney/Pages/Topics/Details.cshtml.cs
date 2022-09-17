using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Topics
{
    public class DetailsModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public DetailsModel(StudyJourneyDbContext context)
        {
            _context = context;
        }

        public Topic Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic = await _context.Topic
                .Include(t => t.Questions)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TopicID == id);

            return Topic == null ? NotFound() : Page();
        }
    }
}
