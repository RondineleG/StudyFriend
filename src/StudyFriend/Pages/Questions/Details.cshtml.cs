using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        private readonly StudyFriendContext _context;

        public DetailsModel(StudyFriendContext context)
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

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
