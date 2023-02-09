namespace CustomiseIdentity.Models.DTOs.ExamPaperDto
{
    public class UpdateExamPaperDto
    {
        public int ExamPaperId { get; set; }
        public string ExamPaperName { get; set; }
        public int? SubjectId { get; set; }
    }
}
