using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public int StudentRollNumber { get; set; }
        [Required]
        public string StudentEmail { get; set; }
        [Required]
        public string StudentAddress { get; set; }
        [Required]
        public string StudentContactNumber { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        public ICollection<StudentTeacher> StudentTeachers { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }

    }
}
