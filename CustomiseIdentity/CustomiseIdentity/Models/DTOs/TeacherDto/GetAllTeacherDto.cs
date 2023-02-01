namespace CustomiseIdentity.Models.DTOs.TeacherDto
{
    public class GetAllTeacherDto
    {
        public string TeacherId { get; set; }
        public string TeacherUserName { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherContactNumber { get; set; }
        public List<int> SubjectIds { get; set; }
        public List<string> SubjectName { get; set; }
    }
}
