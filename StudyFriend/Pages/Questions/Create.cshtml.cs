using Microsoft.AspNetCore.Mvc;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Questions
{
    public class CreateModel : TopicNamePageModel
    {
        private readonly StudyFriendContext _context;

        public CreateModel(StudyFriendContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? topicId)
        {
            if (topicId != null)
            {
                PopulateTopicsDropDownList(_context, topicId);
                return Page();
            }
            PopulateTopicsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnPostAsync(int? topicId)
        {
            if (!ModelState.IsValid)
            {
                PopulateTopicsDropDownList(_context, topicId);
                return Page();
            }

            var emptyQuestion = new Question();

            if (await TryUpdateModelAsync(
                emptyQuestion,
                "question",
                s => s.QuestionID, s => s.TopicID, s => s.Body, s => s.Topic))
            {
                _context.Question.Add(emptyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToPage("../Questions/Details", new { id = emptyQuestion.QuestionID });
            }

            PopulateTopicsDropDownList(_context, emptyQuestion.QuestionID);
            return Page();
        }
    }
}