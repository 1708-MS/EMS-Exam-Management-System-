using EMS_Api_Identity_React.Models.Identity;

namespace EMS_Api_Identity_React.Models
{
    public class AnswerSheet
    {
        public int AnswerSheetId { get; set; }
        public int ExamPaperId { get; set; }
        public ExamPaper ExamPaper { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public bool IsChecked { get; set; }
    }
}
