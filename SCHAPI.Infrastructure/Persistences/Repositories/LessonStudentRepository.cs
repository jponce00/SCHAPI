using Microsoft.EntityFrameworkCore;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class LessonStudentRepository : GenericRepository<LessonStudent>, ILessonStudentRepository
    {
        public LessonStudentRepository(SCHAPIContext context)
            : base(context)
        {
        }

        public async Task<BaseEntityResponse<LessonStudent>> ListLessonStudents(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<LessonStudent>();

            var lessonStudents = GetEntityQuery()
                .Include(sl => sl.Lesson).ThenInclude(ls => ls.Subject)
                .Include(sl => sl.Lesson).ThenInclude(ls => ls.Schedule)
                .Include(sl => sl.Lesson).ThenInclude(ls => ls.Classroom)
                .Include(sl => sl.Student)
                .AsNoTracking();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        lessonStudents = lessonStudents.Where(sl => sl.LessonId == Convert.ToInt32(filters.TextFilter));
                        break;
                    case 2:
                        lessonStudents = lessonStudents.Where(ls => ls.StudentId == Convert.ToInt32(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter != null)
            {
                lessonStudents = lessonStudents.Where(sl => sl.State.Equals(filters.StateFilter));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await lessonStudents.CountAsync();
            response.Items = await Ordering(filters, lessonStudents).ToListAsync();

            return response;
        }
    }
}
