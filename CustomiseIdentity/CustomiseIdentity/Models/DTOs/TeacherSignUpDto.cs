namespace CustomiseIdentity.Models.DTOs
{
    public class TeacherSignUpDto
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherContactNumber { get; set; }
        public int SubjectId { get; set; }
        public SubjectDto SubjectDto { get; set; }
    }
}
