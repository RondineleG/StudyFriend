using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Questions
{
    public class EditModel : TopicNamePageModel
    {
        private readonly StudyFriendContext _context;

        public EditModel(StudyFriendContext context)
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
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuestionID == id);

            if (Question == null)
            {
                return NotFound();
            }

            // Select current QuestionID
            PopulateTopicsDropDownList(_context, Question.TopicID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                PopulateTopicsDropDownList(_context, Question.TopicID);
                return Page();
            }

            var questionToUpdate = await _context.Question.FindAsync(id);

            if (await TryUpdateModelAsync(
                questionToUpdate,
                "question",
                q => q.Body, q => q.TopicID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateTopicsDropDownList(_context, questionToUpdate.TopicID);
            return Page();
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.QuestionID == id);
        }
    }
}
