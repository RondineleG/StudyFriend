using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public DeleteModel(StudyJourneyDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question
                .AsNoTracking()
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuestionID == id);

            return Question == null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.QuestionID == id);

            if (Question != null)
            {
                _context.Question.Remove(Question);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
