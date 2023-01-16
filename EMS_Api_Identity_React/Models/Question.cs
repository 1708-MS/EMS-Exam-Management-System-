namespace EMS_Api_Identity_React.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string MCQsOptions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public QuestionType QuestionType { get; set; }
    }
    public enum QuestionType
    {
        ShortAnswer = 1,
        LongAnswer = 2,
        MCQs = 3
    }
}
