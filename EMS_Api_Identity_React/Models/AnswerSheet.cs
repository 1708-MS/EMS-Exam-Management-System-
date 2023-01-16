namespace EMS_Api_Identity_React.Models
{
    public class AnswerSheet
    {
        public int AnswerSheetId { get; set; }
        public int ExamPaperId { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
