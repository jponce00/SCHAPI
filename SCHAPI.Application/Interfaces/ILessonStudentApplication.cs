using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.LessonStudent.Request;
using SCHAPI.Application.Dtos.LessonStudent.Response;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Interfaces
{
    public interface ILessonStudentApplication
    {
        Task<BaseResponse<BaseEntityResponse<LessonStudentResponseDto>>> ListLessonStudents(BaseFiltersRequest filters);

        Task<BaseResponse<bool>> RegisterLessonStudent(LessonStudentRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveLessonStudents(int lessonStudentId);
    }
}
