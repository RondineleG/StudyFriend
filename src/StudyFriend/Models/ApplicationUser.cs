using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StudyFriend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Topic> Topics { get; set; }
    }
}
