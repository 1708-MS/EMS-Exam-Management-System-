using EMS_Api_Identity_React.Data;
using Microsoft.EntityFrameworkCore;

namespace EMS_Api_Identity_React.Models.DTOs
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? ExamPaperId { get; set; }
    }
}
