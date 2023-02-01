namespace CustomiseIdentity.Models.DTOs.QuestionDto
{
    public class CreateQuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public float QuestionMarks { get; set; }
        public int MCQOptionId { get; set; }
        public int AnswerId { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
