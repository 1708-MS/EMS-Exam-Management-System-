using CustomiseIdentity.Identity;

namespace CustomiseIdentity.Models
{
    public class AnswerSheet
    {
        public int AnswerSheetId { get; set; }
        public int? ExamPaperId { get; set; }
        public ExamPaper ExamPaper { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public bool IsChecked { get; set; }
    }
}
