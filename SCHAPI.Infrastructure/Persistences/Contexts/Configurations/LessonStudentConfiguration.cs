using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class LessonStudentConfiguration : IEntityTypeConfiguration<LessonStudent>
    {
        public void Configure(EntityTypeBuilder<LessonStudent> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.LessonId, e.StudentId })
                .IsUnique();

            builder.HasOne(e => e.Lesson)
                .WithMany(e => e.Students)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(e => e.Student)
                .WithMany(e => e.Lessons)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasQueryFilter(e => e.AuditDeleteUser == null && e.AuditDeleteDate == null);
        }
    }
}
