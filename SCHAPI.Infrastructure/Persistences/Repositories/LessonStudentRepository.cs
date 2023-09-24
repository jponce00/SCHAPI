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

        public async Task<BaseEntityResponse<LessonStudent>> ListStudentsOfLesson(BaseFiltersRequest filters, int lessonId)
        {
            var response = new BaseEntityResponse<LessonStudent>();

            var studentsOfLesson = GetEntityQuery(sl => sl.LessonId.Equals(lessonId))
                .Include(sl => sl.Student)
                .AsNoTracking();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        studentsOfLesson = studentsOfLesson.Where(sl => sl.Student.Name.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter != null)
            {
                studentsOfLesson = studentsOfLesson.Where(sl => sl.State.Equals(filters.StateFilter));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await studentsOfLesson.CountAsync();
            response.Items = await Ordering(filters, studentsOfLesson).ToListAsync();

            return response;
        }
    }
}
