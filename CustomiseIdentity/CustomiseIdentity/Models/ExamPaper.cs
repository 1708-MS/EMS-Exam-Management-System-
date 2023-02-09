using CustomiseIdentity.Identity;

namespace CustomiseIdentity.Models
{
    public class ExamPaper
    {
        public int ExamPaperId { get; set; }
        public string ExamPaperName { get; set; }
        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<AnswerSheet> AnswerSheets { get; set; }
    }
}