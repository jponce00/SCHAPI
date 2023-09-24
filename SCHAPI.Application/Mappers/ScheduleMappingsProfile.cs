using AutoMapper;
using SCHAPI.Application.Dtos.Schedule.Response;
using SCHAPI.Domain.Entities;

namespace SCHAPI.Application.Mappers
{
    public class ScheduleMappingsProfile : Profile
    {
        public ScheduleMappingsProfile()
        {
            CreateMap<Schedule, ScheduleSelectResponseDto>()
                .ForMember(s => s.ScheduleId, s => s.MapFrom(y => y.Id))
                .ForMember(s => s.StartHour, s => s.MapFrom(y => y.StartHour.ToString()))
                .ReverseMap();
        }
    }
}
