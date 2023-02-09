namespace CustomiseIdentity.Models.DTOs.ExamPaperDto
{
    public class CreateExamPaperDto
    {
        public int SubjectId { get; set; }
        public string ExamPaperName { get; set; }
        public List<string> ApplicationUserId { get; set; }
    }
}
