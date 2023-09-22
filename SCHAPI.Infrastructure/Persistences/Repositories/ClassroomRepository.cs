using Microsoft.EntityFrameworkCore;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class ClassroomRepository : GenericRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(SCHAPIContext context) : base(context)
        {
        }

        public async Task<BaseEntityResponse<Classroom>> ListClassrooms(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Classroom>();

            var classrooms = GetEntityQuery();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        classrooms = classrooms.Where(c => c.Name.Contains(filters.TextFilter));
                        break;
                }
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await classrooms.CountAsync();
            response.Items = await Ordering(filters, classrooms).ToListAsync();

            return response;
        }
    }
}
