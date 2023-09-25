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
        private readonly SCHAPIContext _context;

        public SubjectRepository(SCHAPIContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Subject>> ListSubjects(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Subject>();

            var subjects = GetEntityQuery().AsNoTracking();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        subjects = subjects.Where(s => s.SubjectCode.Contains(filters.TextFilter));
                        break;
                    case 2:
                        subjects = subjects.Where(s => s.Name.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter != null)
            {
                subjects = subjects.Where(s => s.State.Equals(filters.StateFilter));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await subjects.CountAsync();
            response.Items = await Ordering(filters, subjects).ToListAsync();

            return response;
        }

        public override async Task<bool> RemoveAsync(int id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Lessons).ThenInclude(l => l.Students)
                .FirstOrDefaultAsync(s => s.Id.Equals(id));

            _context.Remove(subject!);

            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }
    }
}
