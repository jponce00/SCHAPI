using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.ClassroomCode)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.Property(e => e.ClassroomCode)
                .HasComputedColumnSql("CONCAT('AU', CAST(Id AS VARCHAR))");

            builder.HasQueryFilter(e => e.AuditDeleteUser == null && e.AuditDeleteDate == null);
        }
    }
}
