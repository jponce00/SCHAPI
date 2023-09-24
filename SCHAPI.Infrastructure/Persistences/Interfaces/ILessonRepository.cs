using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Infrastructure.Persistences.Interfaces
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<BaseEntityResponse<Lesson>> ListLessons(BaseFiltersRequest filters);

        Task<IEnumerable<Lesson>> ListSelectLessons();
    }
}
