using Microsoft.EntityFrameworkCore;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Persistences.Contexts.Interceptors;
using System.Reflection;

namespace SCHAPI.Infrastructure.Persistences.Contexts
{
    public class SCHAPIContext : DbContext
    {
        public SCHAPIContext()
        {
        }

        public SCHAPIContext(DbContextOptions<SCHAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Classroom> Classrooms {  get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonStudent> LessonStudents { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new DeleteAuditableInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
