

using CustomiseIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomiseIdentity.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public string Token { get; set; } = string.Empty;
        [NotMapped]
        public string Role { get; set; } = string.Empty;
        public string Address { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<ExamPaper> ExamPapers { get; set; }
        public ICollection<AnswerSheet> AnswerSheets { get; set; }
    }
}
