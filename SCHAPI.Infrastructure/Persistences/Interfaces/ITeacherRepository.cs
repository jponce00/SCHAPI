using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Infrastructure.Persistences.Interfaces
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<BaseEntityResponse<Teacher>> ListTeachers(BaseFiltersRequest filters);
    }
}
