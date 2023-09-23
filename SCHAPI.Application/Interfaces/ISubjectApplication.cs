using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Subject.Request;
using SCHAPI.Application.Dtos.Subject.Response;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Interfaces
{
    public interface ISubjectApplication
    {
        Task<BaseResponse<BaseEntityResponse<SubjectResponseDto>>> ListSubjects(BaseFiltersRequest filters);

        Task<BaseResponse<IEnumerable<SubjectSelectResponseDto>>> ListSelectSubjects();

        Task<BaseResponse<SubjectResponseDto>> SubjectById(int subjectId);

        Task<BaseResponse<bool>> RegisterSubject(SubjectRequestDto requestDto);

        Task<BaseResponse<bool>> EditSubject(int subjectId, SubjectRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveSubject(int subjectId);
    }
}
