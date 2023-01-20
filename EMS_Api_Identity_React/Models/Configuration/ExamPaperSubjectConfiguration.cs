using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS_Api_Identity_React.Models.Configuration
{
    public class ExamPaperSubjectConfiguration : IEntityTypeConfiguration<ExamPaper>
    {
        public void Configure(EntityTypeBuilder<ExamPaper> builder)
        {
            builder.HasOne(ExamPaper => ExamPaper.Subject)
               .WithOne(Subject => Subject.ExamPaper)
               .HasForeignKey<Subject>(Subject => Subject.ExamPaperId);
        }
    }
}
