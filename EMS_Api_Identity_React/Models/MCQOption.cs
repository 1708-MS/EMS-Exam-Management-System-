namespace EMS_Api_Identity_React.Models
{
    public class MCQOption
    {
        public int MCQOptionId { get; set; }
        public string SubmittedAnswerOfMCQ { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
