using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyJourney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyJourney.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.Users = new List<UsersViewModel>();
        }
        public List<UsersViewModel> Users { get; set; }

        public async Task OnGet()
        {
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            foreach (ApplicationUser user in users)
            {
                UsersViewModel thisViewModel = new UsersViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                Users.Add(thisViewModel);
            }
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
    public class UsersViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
