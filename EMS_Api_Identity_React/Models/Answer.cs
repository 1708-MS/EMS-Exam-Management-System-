namespace EMS_Api_Identity_React.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public float AnswerMarks { get; set; }
        public int AnswerSheetId { get; set; }
        public AnswerSheet AnswerSheet { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public ICollection<MCQOption> MCQOptions { get; set; }
    }
}