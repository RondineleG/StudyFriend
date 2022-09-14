using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyFriend.Models
{
    public class Question
    {
        public int QuestionID { get; set; }

        public int TopicID { get; set; }

        [StringLength(240, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        [Required(ErrorMessage = "Please provide a question.")]
        public string Body { get; set; }

        public Topic Topic { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
