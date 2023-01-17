using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Api_Identity_React.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public string Address { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<ExamPaper> ExamPapers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<MCQOptions> MCQOptions { get; set; }
        public ICollection<AnswerSheet> AnswerSheets { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Grades> Grades { get; set; }
    }
}
