using EMS_Api_Identity_React.Identity;
using EMS_Api_Identity_React.Models;
using EMS_Api_Identity_React.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EMS_Api_Identity_React.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ExamPaper> ExamPapers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<MCQOption> MCQOptions { get; set; }
        public DbSet<AnswerSheet> AnswerSheets { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}
