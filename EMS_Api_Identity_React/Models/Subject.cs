using EMS_Api_Identity_React.Models.Identity;

namespace EMS_Api_Identity_React.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ExamPaperId { get; set; }
        public ExamPaper ExamPaper { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
