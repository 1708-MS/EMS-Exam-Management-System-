namespace CustomiseIdentity.Models.DTOs
{
    public class GetAllTeacherDto
    {
        public string TeacherId { get; set; }
        public string TeacherUserName { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherContactNumber { get; set; }
        public List<int> SubjectIds { get; set; }
        public string SubjectName { get; set; }
    }
}
