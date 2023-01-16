namespace EMS_Api_Identity_React.Models
{
    public class ExamPaper
    {
        public int ExamPaperId { get; set; }
        public Subject Subject { get; set; }
        public int TeacherId { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}
