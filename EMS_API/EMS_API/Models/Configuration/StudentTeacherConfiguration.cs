using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS_API.Models.Configuration
{
    public class StudentTeacherConfiguration : IEntityTypeConfiguration<StudentTeacher>
    {
        public void Configure(EntityTypeBuilder<StudentTeacher> builder)
        {
            //To configure many to many relationship between Student and Teacher
            builder.HasKey(StudentTeacher => new { StudentTeacher.StudentId, StudentTeacher.TeacherId });
        }
    }
}
