namespace CustomiseIdentity.Models.DTOs.QuestionDto
{
    public class GetAllQuestionsDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public float QuestionMarks { get; set; }
        public List<int> MCQOptionId { get; set; }
        public int? ExamPaperId { get; set; }
        public int? AnswerId { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
