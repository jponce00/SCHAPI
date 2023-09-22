using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Infrastructure.Persistences.Interfaces
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<BaseEntityResponse<Subject>> ListSubjects(BaseFiltersRequest filters);
    }
}
