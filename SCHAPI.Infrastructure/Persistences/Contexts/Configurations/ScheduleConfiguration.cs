using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.StartHour)
                .HasColumnType("time");

            builder.Property(e => e.EndHour)
                .HasColumnType("time");

            builder.HasQueryFilter(e => e.AuditDeleteUser == null && e.AuditDeleteDate == null);
        }
    }
}
