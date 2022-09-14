using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Data;
using System.Linq;
using System.Security.Claims;

namespace StudyFriend.Pages.Answers
{
    public class QuestionBodyPageModel : PageModel
    {
        public SelectList QuestionBodySL { get; set; }

        public void PopulateQuestionsDropDownList(StudyFriendContext _context,
            object selectedQuestion = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var questionsQuery = from q in _context.Question
                                 join t in _context.Topic
                                 on q.TopicID equals t.TopicID
                                 where t.UserId == userId
                                 select q;

            QuestionBodySL = new SelectList(questionsQuery.AsNoTracking(),
                "QuestionID", "Body", selectedQuestion);
        }
    }
}
