using Microsoft.EntityFrameworkCore;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly SCHAPIContext _context;

        public TeacherRepository(SCHAPIContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Teacher>> ListTeachers(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Teacher>();

            var teachers = GetEntityQuery(t => t.AuditDeleteUser == null && t.AuditDeleteDate == null).AsNoTracking();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        teachers = teachers.Where(t => t.TeacherCode.Contains(filters.TextFilter));
                        break;
                    case 2:
                        teachers = teachers.Where(t => t.Name.Contains(filters.TextFilter));
                        break;
                    case 3:
                        teachers = teachers.Where(t => t.Phone.Contains(filters.TextFilter));
                        break;
                    case 4:
                        teachers = teachers.Where(t => t.Email.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter != null)
            {
                teachers = teachers.Where(t => t.State.Equals(filters.StateFilter));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await teachers.CountAsync();
            response.Items = await Ordering(filters, teachers).ToListAsync();

            return response;
        }

        public override async Task<bool> RemoveAsync(int teacherId)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Lessons.Where(t => t.AuditDeleteUser == null && t.AuditDeleteDate == null))
                .FirstOrDefaultAsync(t => t.Id.Equals(teacherId));

            _context.RemoveRange(teacher!.Lessons);
            _context.Remove(teacher);

            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }
    }
}
