using CustomiseIdentity.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace CustomiseIdentity.Models.DTOs.QuestionDto
{
        public class CreateQuestionDto
        {
            public int QuestionId { get; set; }
            public string QuestionText { get; set; }
            public int QuestionMarks { get; set; }
            public QuestionType QuestionType { get; set; }
            public int? ExamPaperId { get; set; }
            public List<MCQOptionDto> MCQOptions { get; set; }
        }
        public class MCQOptionDto
        {
            public string MCQOptionsOfQuestion { get; set; }
        }
}