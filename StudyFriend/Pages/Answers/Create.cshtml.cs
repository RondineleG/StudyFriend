using Microsoft.AspNetCore.Mvc;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Answers
{
    public class CreateModel : QuestionBodyPageModel
    {
        private readonly StudyFriendContext _context;

        public CreateModel(StudyFriendContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? questionId)

        {
            if (questionId != null)
            {
                PopulateQuestionsDropDownList(_context, questionId);
                return Page();
            }
            PopulateQuestionsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Answer Answer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateQuestionsDropDownList(_context);
                return Page();
            }

            var emptyAnswer = new Answer();

            if (await TryUpdateModelAsync(
                emptyAnswer,
                "answer",
                s => s.AnswerID, s => s.QuestionID, s => s.Body, s => s.Question))
            {
                _context.Answer.Add(emptyAnswer);
                await _context.SaveChangesAsync();
                return RedirectToPage("../Questions/Details", new { id = emptyAnswer.QuestionID });
            }

            PopulateQuestionsDropDownList(_context, emptyAnswer.AnswerID);
            return Page();
        }
    }
}