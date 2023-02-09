namespace CustomiseIdentity.Models.DTOs.SubjectDto
{
    public class UpdateSubjectDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? ExamPaperId { get; set; }
    }
}
