using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Topics
{
    public class DetailsModel : PageModel
    {
        private readonly StudyFriendContext _context;

        public DetailsModel(StudyFriendContext context)
        {
            _context = context;
        }

        public Topic Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic = await _context.Topic
                .Include(t => t.Questions)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TopicID == id);

            if (Topic == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
