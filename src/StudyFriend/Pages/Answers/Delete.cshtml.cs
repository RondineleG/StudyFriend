using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Answers
{
    public class DeleteModel : PageModel
    {
        private readonly StudyFriendContext _context;

        public DeleteModel(StudyFriendContext context)
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

            if (Answer == null)
            {
                return NotFound();
            }
            return Page();
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
