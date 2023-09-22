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

            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.Code)
                .HasColumnName("TeacherCode");

            builder.HasIndex(e => e.Name)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(200);

            builder.HasIndex(e => e.Phone)
                .IsUnique();

            builder.Property(e => e.Phone)
                .HasMaxLength(50);

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.Property(e => e.Email)
                .HasMaxLength(100);

            builder.Property(e => e.Code)
                .HasComputedColumnSql("CONCAT('MAE', RIGHT('000' + CAST(Id AS VARCHAR), 3))");
        }
    }
}
