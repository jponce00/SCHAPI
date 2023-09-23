using AutoMapper;
using SCHAPI.Application.Dtos.Teacher.Request;
using SCHAPI.Application.Dtos.Teacher.Response;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Mappers
{
    public class TeacherMappingsProfile : Profile
    {
        public TeacherMappingsProfile()
        {
            CreateMap<Teacher, TeacherResponseDto>()
                .ForMember(t => t.TeacherId, t => t.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Teacher>, BaseEntityResponse<TeacherResponseDto>>()
                .ReverseMap();

            CreateMap<TeacherRequestDto, Teacher>();

            CreateMap<Teacher, TeacherSelectResponseDto>()
                .ForMember(t => t.TeacherId, t => t.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
