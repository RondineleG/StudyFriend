using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Answers
{
    public class DetailsModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public DetailsModel(StudyJourneyDbContext context)
        {
            _context = context;
        }

        public Answer Answer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _context.Answer
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerID == id);

            return Answer == null ? NotFound() : Page();
        }
    }
}
