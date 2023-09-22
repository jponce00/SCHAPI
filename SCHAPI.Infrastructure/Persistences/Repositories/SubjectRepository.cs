using Microsoft.EntityFrameworkCore;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(SCHAPIContext context)
            : base(context)
        {
        }

        public async Task<BaseEntityResponse<Subject>> ListSubjects(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Subject>();

            var subjects = GetEntityQuery();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        subjects = subjects.Where(s => s.Name.Contains(filters.TextFilter));
                        break;
                }
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await subjects.CountAsync();
            response.Items = await Ordering(filters, subjects).ToListAsync();

            return response;
        }
    }
}
