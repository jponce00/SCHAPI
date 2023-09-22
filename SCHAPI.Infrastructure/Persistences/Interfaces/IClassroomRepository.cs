using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Infrastructure.Persistences.Interfaces
{
    public interface IClassroomRepository : IGenericRepository<Classroom>
    {
        Task<BaseEntityResponse<Classroom>> ListClassrooms(BaseFiltersRequest filters);
    }
}
