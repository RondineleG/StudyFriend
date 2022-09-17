using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;

using System.Linq;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Topics
{
    public class EditModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(StudyJourneyDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Topic Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic = await _context.Topic.FirstOrDefaultAsync(m => m.TopicID == id);

            return Topic == null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationUser currUser = await _userManager.GetUserAsync(User);
            Topic.UserId = currUser.Id;
            _context.Attach(Topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(Topic.TopicID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.TopicID == id);
        }
    }
}
