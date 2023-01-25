﻿namespace EMS_Api_Identity_React.Models.DTOs
{
    public class TeacherDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public SubjectDto Subject { get; set; }
        public int SubjectId { get; set; }
    }

}
