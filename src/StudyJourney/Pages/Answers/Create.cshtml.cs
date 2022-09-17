using Microsoft.AspNetCore.Mvc;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Answers
{
    public class CreateModel : QuestionBodyPageModel
    {
        private readonly StudyJourneyDbContext _context;

        public CreateModel(StudyJourneyDbContext context)
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

            Answer emptyAnswer = new Answer();

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