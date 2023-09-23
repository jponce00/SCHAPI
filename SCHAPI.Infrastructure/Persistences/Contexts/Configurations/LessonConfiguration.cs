using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.LessonCode)
                .IsUnique();

            builder.HasIndex(e => new { e.ScheduleId, e.TeacherId })
                .IsUnique();

            builder.HasIndex(e => new { e.ScheduleId, e.ClassroomId })
                .IsUnique();

            builder.HasOne(e => e.Schedule)
                .WithMany()
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Teacher)
                .WithMany()
                .HasForeignKey(e => e.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Subject)
                .WithMany()
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Classroom)
                .WithMany()
                .HasForeignKey(e => e.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.LessonCode)
                .HasComputedColumnSql("CONCAT('CLA', RIGHT('000' + CAST(Id AS VARCHAR), 3))");
        }
    }
}
