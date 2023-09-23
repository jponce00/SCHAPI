using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Student.Request;
using SCHAPI.Application.Dtos.Student.Response;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Interfaces
{
    public interface IStudentApplication
    {
        Task<BaseResponse<BaseEntityResponse<StudentResponseDto>>> ListStudents(BaseFiltersRequest filters);

        Task<BaseResponse<StudentResponseDto>> StudentById(int studentId);

        Task<BaseResponse<bool>> RegisterStudent(StudentRequestDto requestDto);

        Task<BaseResponse<bool>> EditStudent(int studentId, StudentRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveStudent(int studentId);
    }
}
