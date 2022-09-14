using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudyFriend.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "An app to help you study";
        }
    }
}
