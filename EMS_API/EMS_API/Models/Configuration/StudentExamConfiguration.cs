using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS_API.Models.Configuration
{
    public class StudentExamConfiguration : IEntityTypeConfiguration<StudentExam>
    {
        public void Configure(EntityTypeBuilder<StudentExam> builder)
        {
            builder.HasOne(StudentExam => StudentExam.ExamPaper)
               .WithMany(ExamPaper => ExamPaper.StudentExams)
               .HasForeignKey(StudentExam => StudentExam.ExamPaperId);
                builder.HasOne(StudentExam => StudentExam.Student)
                .WithMany(Student => Student.StudentExams)
                .HasForeignKey(StudentExam => StudentExam.StudentId);
        }
    }
}
