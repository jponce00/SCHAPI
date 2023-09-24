using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Schedule.Response;

namespace SCHAPI.Application.Interfaces
{
    public interface IScheduleApplication
    {
        Task<BaseResponse<IEnumerable<ScheduleSelectResponseDto>>> ListSelectSchedules();
    }
}
