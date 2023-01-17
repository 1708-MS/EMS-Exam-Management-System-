namespace EMS_Api_Identity_React.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<MCQOptions> MCQOptions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
