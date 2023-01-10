using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class StudentTeacher
    {
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
    }
}
