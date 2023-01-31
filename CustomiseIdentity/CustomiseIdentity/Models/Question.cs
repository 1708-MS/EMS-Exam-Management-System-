namespace CustomiseIdentity.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public float QuestionMarks { get; set; }
        public ICollection<MCQOption> MCQOptions { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
