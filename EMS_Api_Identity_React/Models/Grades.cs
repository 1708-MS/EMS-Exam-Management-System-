using EMS_Api_Identity_React.Models.Identity;

namespace EMS_Api_Identity_React.Models
{
    public class Grades
    {
        public int GradesId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ExamPaperId { get; set; }
        public ExamPaper ExamPaper { get; set; }
        public int TotalMarks { get; set; }
        public int LongAnswerMarks { get; set; }
        public int ShortAnswerMarks { get; set; }
        public int MCQsMarks { get; set; }
        public int MCQsNegativeMarks { get; set; }
        public int TotalMarksObtained { get; set; }
    }
}
