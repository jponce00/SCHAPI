using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Classroom.Request;
using SCHAPI.Application.Dtos.Classroom.Response;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Interfaces
{
    public interface IClassroomApplication
    {
        Task<BaseResponse<BaseEntityResponse<ClassroomResponseDto>>> ListClassrooms(BaseFiltersRequest filters);

        Task<BaseResponse<IEnumerable<ClassroomSelectResponseDto>>> ListSelectClassrooms();

        Task<BaseResponse<ClassroomResponseDto>> ClassroomById(int classroomId);

        Task<BaseResponse<bool>> RegisterClassroom(ClassroomRequestDto requestDto);

        Task<BaseResponse<bool>> EditClassroom(int classroomId, ClassroomRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveClassroom(int classroomId);
    }
}
