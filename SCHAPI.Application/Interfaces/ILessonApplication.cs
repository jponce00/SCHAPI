using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Lesson.Request;
using SCHAPI.Application.Dtos.Lesson.Response;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Interfaces
{
    public interface ILessonApplication
    {
        Task<BaseResponse<BaseEntityResponse<LessonResponseDto>>> ListLessons(BaseFiltersRequest filters);

        Task<BaseResponse<IEnumerable<LessonSelectResponseDto>>> ListSelectLessons();

        Task<BaseResponse<LessonResponseDto>> LessonById(int lessonId);

        Task<BaseResponse<bool>> RegisterLesson(LessonRequestDto requestDto);

        Task<BaseResponse<bool>> EditLesson(int lessonId, LessonRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveLesson(int lessonId);
    }
}
