using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code)
                .HasColumnName("StudentCode");

            builder.HasIndex(e => e.Code)
                .IsUnique();

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
                .HasComputedColumnSql("CONCAT('ALU', RIGHT('000' + CAST(Id AS VARCHAR), 3))");
        }
    }
}
