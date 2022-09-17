using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public DetailsModel(StudyJourneyDbContext context)
        {
            _context = context;
        }

        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question
                .Include(q => q.Topic)
                .Include(q => q.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.QuestionID == id);

            return Question == null ? NotFound() : Page();
        }
    }
}
