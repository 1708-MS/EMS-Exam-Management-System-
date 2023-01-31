namespace EMS_Api_Identity_React.Models.DTOs
{
    public class TeacherDto
    {
        public string TeacherId { get; set; }
        public string TeacherUserName { get; set; }
        public string TeacherPassword { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherContactNumber { get; set; }
        public SubjectDto SubjectDto { get; set; }
    }

}
