using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class StudentSubject
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
