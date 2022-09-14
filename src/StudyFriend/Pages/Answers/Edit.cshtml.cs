using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Answers
{
    public class EditModel : QuestionBodyPageModel
    {
        private readonly StudyFriendContext _context;

        public EditModel(StudyFriendContext context)
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

            PopulateQuestionsDropDownList(_context, Answer.QuestionID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Answer = await _context.Answer
               .Include(a => a.Question).FirstOrDefaultAsync(m => m.AnswerID == id);

            if (!ModelState.IsValid)
            {
                PopulateQuestionsDropDownList(_context, Answer.QuestionID);
                return Page();
            }

            var answerToUpdate = await _context.Answer.FindAsync(id);

            if (await TryUpdateModelAsync(
                answerToUpdate,
                "answer",
                a => a.Body, a => a.QuestionID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("../Questions/Details", new { id = answerToUpdate.QuestionID });
            }

            PopulateQuestionsDropDownList(_context, answerToUpdate.QuestionID);
            return Page();
        }
    }
}
