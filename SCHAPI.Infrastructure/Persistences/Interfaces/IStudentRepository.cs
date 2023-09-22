using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Infrastructure.Persistences.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<BaseEntityResponse<Student>> ListStudents(BaseFiltersRequest filters);
    }
}
