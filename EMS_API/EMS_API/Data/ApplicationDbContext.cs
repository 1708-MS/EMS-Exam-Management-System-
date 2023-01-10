using EMS_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EMS_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<StudentSubject>().HasKey(t => new { t.StudentId, t.SubjectId });
            modelBuilder.Entity<StudentTeacher>().HasKey(t => new { t.StudentId, t.TeacherId });
        }

    }
}
