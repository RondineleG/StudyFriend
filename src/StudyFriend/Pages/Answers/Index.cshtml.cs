using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Data;
using StudyFriend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyFriend.Pages.Answers
{
    public class IndexModel : PageModel
    {
        private readonly StudyFriendContext _context;

        public IndexModel(StudyFriendContext context)
        {
            _context = context;
        }

        public IList<Answer> Answer { get; set; }

        public async Task OnGetAsync()
        {
            Answer = await _context.Answer
                .Include(a => a.Question).ToListAsync();
        }
    }
}
