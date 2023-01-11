using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        public ICollection<ExamPaper> ExamPapers { get; set; }

    }
}
