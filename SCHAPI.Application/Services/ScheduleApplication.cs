using AutoMapper;
using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Schedule.Response;
using SCHAPI.Application.Interfaces;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;
using WatchDog;

namespace SCHAPI.Application.Services
{
    public class ScheduleApplication : IScheduleApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<ScheduleSelectResponseDto>>> ListSelectSchedules()
        {
            var response = new BaseResponse<IEnumerable<ScheduleSelectResponseDto>>();

            try
            {
                var schedules = await _unitOfWork.Schedule.GetAllAsync();

                if (schedules != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<ScheduleSelectResponseDto>>(schedules);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}
