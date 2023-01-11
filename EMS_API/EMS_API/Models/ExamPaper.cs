namespace EMS_API.Models
{
    public class ExamPaper
    {
        public int ExamPaperId { get; set; }
        public Subject Subject { get; set; }
        public string LongQuestion { get; set; }
        public string LongAnswer { get; set; }
        public string ShortQuestion { get; set; }
        public string ShortAnswer { get; set; }
        public string MCQs { get; set; }
        public string MCQOptions { get; set; }
        public List<int> MCQAnswerKey { get; set; }
        public int TeacherId { get; set; }  
        public ICollection<StudentExam> StudentExams { get; set; }
    }
}
