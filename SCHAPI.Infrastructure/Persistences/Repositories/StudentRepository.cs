using Microsoft.EntityFrameworkCore;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(SCHAPIContext context)
            : base(context)
        {
        }

        public async Task<BaseEntityResponse<Student>> ListStudents(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Student>();

            var students = GetEntityQuery();

            if (filters.NumFilter != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        students = students.Where(s => s.Name.Contains(filters.TextFilter));
                        break;
                    case 2:
                        students = students.Where(s => s.Phone.Contains(filters.TextFilter));
                        break;
                    case 3:
                        students = students.Where(s => s.Email.Contains(filters.TextFilter));
                        break;
                }
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await students.CountAsync();
            response.Items = await Ordering(filters, students).ToListAsync();

            return response;
        }
    }
}
