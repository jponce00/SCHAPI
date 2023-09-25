using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Infrastructure.Persistences.Interfaces
{
    public interface ILessonStudentRepository : IGenericRepository<LessonStudent>
    {
        Task<BaseEntityResponse<LessonStudent>> ListLessonStudents(BaseFiltersRequest filters);
    }
}
