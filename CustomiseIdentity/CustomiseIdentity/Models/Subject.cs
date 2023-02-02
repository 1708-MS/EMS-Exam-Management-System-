using CustomiseIdentity.Identity;

namespace CustomiseIdentity.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? ExamPaperId { get; set; }
        public ExamPaper ExamPaper { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}