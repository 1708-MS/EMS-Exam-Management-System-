using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS_Api_Identity_React.Models.Configuration
{
    public class QuestionAnswerConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(Question => Question.Answer)
               .WithOne(Answer => Answer.Question)
               .HasForeignKey<Answer>(Answer => Answer.QuestionId);
        }
    }
}
