using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS_API.Models.Configuration
{
    public class StudentSubjectConfig : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            //To configure many to many relationship between Student and Subject
            builder.HasKey(StudentSubject => new { StudentSubject.StudentId, StudentSubject.SubjectId });
        }
    }
}
