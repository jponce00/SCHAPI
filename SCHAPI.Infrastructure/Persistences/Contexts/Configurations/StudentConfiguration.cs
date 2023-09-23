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

            builder.HasIndex(e => e.StudentCode)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(200);

            builder.Property(e => e.Phone)
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .HasMaxLength(100);

            builder.Property(e => e.StudentCode)
                .HasComputedColumnSql("CONCAT('ALU', RIGHT('000' + CAST(Id AS VARCHAR), 3))");
        }
    }
}
