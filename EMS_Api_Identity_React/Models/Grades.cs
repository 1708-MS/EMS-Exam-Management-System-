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
        public int AnswerSheetId { get; set; }
        public AnswerSheet AnswerSheet { get; set; }
        public double TotalMarks { get; set; }
        public double LongAnswerMarks { get; set; }
        public double ShortAnswerMarks { get; set; }
        public double MCQsMarks { get; set; }
        public double MCQsNegativeMarks { get; set; }
        public double MarksGivenForEachQuestion { get; set; }
        public double TotalMarksObtained { get; set; }
    }
}
