using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StudyJourney.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
