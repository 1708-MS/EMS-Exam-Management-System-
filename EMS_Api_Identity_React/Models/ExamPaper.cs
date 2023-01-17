using EMS_Api_Identity_React.Models.Identity;

namespace EMS_Api_Identity_React.Models
{
    public class ExamPaper
    {
        public int ExamPaperId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
