using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyFriend.Models
{
    public class Topic
    {
        public int TopicID { get; set; }

        public string UserId { get; set; }

        [StringLength(80, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        [Required(ErrorMessage = "Please provide a topic.")]
        public string Name { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
