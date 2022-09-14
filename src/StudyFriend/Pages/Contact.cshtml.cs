using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudyFriend.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Say hello to David!";
        }
    }
}
