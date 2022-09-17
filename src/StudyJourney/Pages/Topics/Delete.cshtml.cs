using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Topics
{
    public class DeleteModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;

        public DeleteModel(StudyJourneyDbContext context)
        {
            _context = context;
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic = await _context.Topic.FindAsync(id);

            if (Topic != null)
            {
                _context.Topic.Remove(Topic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
