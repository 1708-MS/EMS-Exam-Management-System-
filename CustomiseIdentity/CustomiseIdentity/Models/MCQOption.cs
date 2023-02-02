namespace CustomiseIdentity.Models
{
    public class MCQOption
    {
        public int MCQOptionId { get; set; }
        public string SubmittedAnswerOfMCQ { get; set; }
        public int? AnswerId { get; set; }
        public Answer Answer { get; set; }
        public Question Question { get; internal set; }
    }
}
