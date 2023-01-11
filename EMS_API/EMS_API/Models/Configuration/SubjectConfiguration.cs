using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS_API.Models.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne(builder => builder.Teacher)
                .WithOne(Teacher => Teacher.Subject)
                .HasForeignKey<Subject>(Subject => Subject.TeacherId);
        }
    }
}
