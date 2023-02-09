namespace CustomiseIdentity.Models.DTOs.ExamPaperDto
{
    public class GetExamPaperDto
    {
        public int ExamPaperId { get; set; }
        public int? SubjectId { get; set; }
        public string ExamPaperName { get; set; }
        public List<string> ApplicationUserId { get; set; }
        public List<int> QuestionId { get; set; }
        public List<Question> Questions { get; set; }
        public List<int> AnswerSheetId { get; set; }
    }
}