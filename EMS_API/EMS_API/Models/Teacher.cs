using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace EMS_API.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherAddress { get; set; }
        public decimal TeacherSalary { get; set; }
       // public string TeacherSubject { get; set; }
        public Subject Subject { get; set; }
        public ICollection<StudentTeacher> StudentTeachers { get; set; }
        public ICollection<ExamPaper> ExamPapers { get; set; }
    }
}