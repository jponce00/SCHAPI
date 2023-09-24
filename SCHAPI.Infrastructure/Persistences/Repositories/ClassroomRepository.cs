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
        private readonly SCHAPIContext _context;

        public ClassroomRepository(SCHAPIContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Classroom>> ListClassrooms(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Classroom>();

            var classrooms = GetEntityQuery().AsNoTracking();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        classrooms = classrooms.Where(c => c.ClassroomCode.Contains(filters.TextFilter));
                        break;
                    case 2:
                        classrooms = classrooms.Where(c => c.Name.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter != null)
            {
                classrooms = classrooms.Where(cl => cl.State.Equals(filters.StateFilter));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await classrooms.CountAsync();
            response.Items = await Ordering(filters, classrooms).ToListAsync();

            return response;
        }

        public override async Task<bool> RemoveAsync(int id)
        {
            var classroom = await _context.Classrooms
                .Include(cl => cl.Lessons)
                .FirstOrDefaultAsync(cl => cl.Id.Equals(id));

            _context.Remove(classroom!);

            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }
    }
}
