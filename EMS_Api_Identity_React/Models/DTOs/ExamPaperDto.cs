namespace EMS_Api_Identity_React.Models.DTOs
{
    public class ExamPaperDto
    {
        public int ExamPaperId { get; set; }
        public int SubjectId { get; set; }
        public List<AnswerSheetDto> AnswerSheets { get; set; }
    }
}
