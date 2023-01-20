using EMS_Api_Identity_React.Models.Identity;

namespace EMS_Api_Identity_React.Models
{
    public class ExamPaper
    {
        public int ExamPaperId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<AnswerSheet> AnswerSheets { get; set; }
    }
}
