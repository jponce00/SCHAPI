using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.TeacherCode)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(200);

            builder.Property(e => e.Phone)
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .HasMaxLength(100);

            builder.HasMany(e => e.Lessons)
                .WithOne(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId);

            builder.Property(e => e.TeacherCode)
                .HasComputedColumnSql("CONCAT('MAE', RIGHT('000' + CAST(Id AS VARCHAR), 3))");
        }
    }
}
