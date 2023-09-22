using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.Code)
                .HasColumnName("SubjectCode");

            builder.HasIndex(e => e.Name)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.Property(e => e.Code)
                .HasComputedColumnSql("CONCAT('MAT', RIGHT('000' + CAST(Id AS VARCHAR), 3))");
        }
    }
}
