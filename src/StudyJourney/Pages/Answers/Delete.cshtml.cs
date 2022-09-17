using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Answers
{
    public class DeleteModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public DeleteModel(StudyJourneyDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Answer Answer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _context.Answer
                .Include(a => a.Question).FirstOrDefaultAsync(m => m.AnswerID == id);

            return Answer == null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _context.Answer.FindAsync(id);

            if (Answer != null)
            {
                _context.Answer.Remove(Answer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Questions/Details", new { id = Answer.QuestionID });
        }
    }
}
