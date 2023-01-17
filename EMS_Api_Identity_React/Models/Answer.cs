namespace EMS_Api_Identity_React.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public List<MCQOptions> MCQOptions { get; set; }
        public string ShortAnswer { get; set; }
        public string LongAnswer { get; set; }
    }
}
