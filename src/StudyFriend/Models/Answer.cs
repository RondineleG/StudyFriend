using System.ComponentModel.DataAnnotations;

namespace StudyFriend.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }

        public int QuestionID { get; set; }

        [StringLength(320, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        [Required(ErrorMessage = "Please provide an answer.")]
        public string Body { get; set; }

        public Question Question { get; set; }
    }
}
