using AutoMapper;
using SCHAPI.Application.Dtos.Student.Request;
using SCHAPI.Application.Dtos.Student.Response;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Mappers
{
    public class StudentMappingsProfile : Profile
    {
        public StudentMappingsProfile()
        {
            CreateMap<Student, StudentResponseDto>()
                .ForMember(s => s.StudentId, s => s.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Student>, BaseEntityResponse<StudentResponseDto>>()
                .ReverseMap();

            CreateMap<StudentRequestDto, Student>();
        }
    }
}
