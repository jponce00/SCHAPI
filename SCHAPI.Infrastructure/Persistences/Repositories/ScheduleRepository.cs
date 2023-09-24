using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(SCHAPIContext context)
            : base(context)
        {
        }
    }
}
