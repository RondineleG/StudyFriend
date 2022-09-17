using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using StudyJourney.Data;
using StudyJourney.Models;
using System.Threading.Tasks;

namespace StudyJourney.Pages.Topics
{
    public class CreateModel : PageModel
    {
        private readonly StudyJourneyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(StudyJourneyDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Topic Topic { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationUser currUser = await _userManager.GetUserAsync(User);

            Topic.UserId = currUser.Id;

            _context.Topic.Add(Topic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}