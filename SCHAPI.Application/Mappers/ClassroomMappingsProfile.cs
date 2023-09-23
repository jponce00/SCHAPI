using AutoMapper;
using SCHAPI.Application.Dtos.Classroom.Request;
using SCHAPI.Application.Dtos.Classroom.Response;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Mappers
{
    public class ClassroomMappingsProfile : Profile
    {
        public ClassroomMappingsProfile()
        {
            CreateMap<Classroom, ClassroomResponseDto>()
                .ForMember(cl => cl.ClassroomId, cl => cl.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Classroom>, BaseEntityResponse<ClassroomResponseDto>>()
                .ReverseMap();

            CreateMap<ClassroomRequestDto, Classroom>();

            CreateMap<Classroom, ClassroomSelectResponseDto>()
                .ForMember(cl => cl.ClassroomId, cl => cl.MapFrom(y => y.Id));
        }
    }
}
