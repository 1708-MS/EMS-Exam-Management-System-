namespace EMS_API.Models
{
    public class StudentExam
    {
        public int StudentExamId { get; set; }
        public int ExamPaperId { get; set; }
        public ExamPaper ExamPaper { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string LongAnswer { get; set; }
        public string ShortAnswer { get; set; }
        public List<int> MCQAnswer { get; set; }
        public int Marks { get; set; }
        public int CheckedByTeacherId { get; set; }
    }
}
