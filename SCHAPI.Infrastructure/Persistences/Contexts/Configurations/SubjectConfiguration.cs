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

            builder.HasIndex(e => e.SubjectCode)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.HasMany(e => e.Lessons)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId);

            builder.Property(e => e.SubjectCode)
                .HasComputedColumnSql("CONCAT('MAT', RIGHT('000' + CAST(Id AS VARCHAR), 3))");
        }
    }
}
