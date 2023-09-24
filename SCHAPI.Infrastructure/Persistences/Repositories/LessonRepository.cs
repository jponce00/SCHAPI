using Microsoft.EntityFrameworkCore;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly SCHAPIContext _context;

        public LessonRepository(SCHAPIContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Lesson>> ListLessons(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Lesson>();

            var lessons = GetEntityQuery(l => l.AuditDeleteUser == null && l.AuditDeleteDate == null)
                .Include(l => l.Schedule)
                .Include(l => l.Teacher)
                .Include(l => l.Subject)
                .Include(l => l.Classroom)
                .AsNoTracking();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        lessons = lessons.Where(l => l.LessonCode.Contains(filters.TextFilter));
                        break;
                    case 2:
                        lessons = lessons.Where(l => l.Teacher.Name.Contains(filters.TextFilter));
                        break;
                    case 3:
                        lessons = lessons.Where(l => l.Subject.Name.Contains(filters.TextFilter));
                        break;
                    case 4:
                        lessons = lessons.Where(l => l.Classroom.Name.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter != null)
            {
                lessons = lessons.Where(l => l.State.Equals(filters.StateFilter));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await lessons.CountAsync();
            response.Items = await Ordering(filters, lessons).ToListAsync();

            return response;
        }

        public Task<IEnumerable<Lesson>> ListSelectLessons()
        {
            return _context.Lessons
                .Where(l => l.AuditDeleteUser == null && l.AuditDeleteDate == null && l.State.Equals((int)StateTypes.Active))
                .Include(l => l.Subject)
                .Include(l => l.Schedule)
                .AsNoTracking()
                .ToListAsync()
                .ContinueWith(d => d.Result.AsEnumerable());
        }
    }
}
