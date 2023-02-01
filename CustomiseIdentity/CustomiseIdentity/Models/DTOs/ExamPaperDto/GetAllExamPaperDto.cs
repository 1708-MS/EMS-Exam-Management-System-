namespace CustomiseIdentity.Models.DTOs.ExamPaperDto
{
    public class GetAllExamPaperDto
    {
        public int ExamPaperId { get; set; }
        public int SubjectId { get; set; }
        public List<string> ApplicationUserId { get; set; }
        public List<int> QuestionId { get; set; }
        public List<int> AnswerSheetId { get; set; }
    }
}
