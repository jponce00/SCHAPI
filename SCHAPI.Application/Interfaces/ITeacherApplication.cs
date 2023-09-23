using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Teacher.Request;
using SCHAPI.Application.Dtos.Teacher.Response;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Interfaces
{
    public interface ITeacherApplication
    {
        Task<BaseResponse<BaseEntityResponse<TeacherResponseDto>>> ListTeachers(BaseFiltersRequest filters);

        Task<BaseResponse<IEnumerable<TeacherSelectResponseDto>>> ListSelectTeachers();

        Task<BaseResponse<TeacherResponseDto>> TeacherById(int teacherId);

        Task<BaseResponse<bool>> RegisterTeacher(TeacherRequestDto requestDto);

        Task<BaseResponse<bool>> EditTeacher(int teacherId, TeacherRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveTeacher(int teacherId);
    }
}
