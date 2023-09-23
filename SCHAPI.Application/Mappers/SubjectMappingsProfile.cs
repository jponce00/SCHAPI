using AutoMapper;
using SCHAPI.Application.Dtos.Subject.Request;
using SCHAPI.Application.Dtos.Subject.Response;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Mappers
{
    public class SubjectMappingsProfile : Profile
    {
        public SubjectMappingsProfile()
        {
            CreateMap<Subject, SubjectResponseDto>()
                .ForMember(s => s.SubjectId, s => s.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Subject>, BaseEntityResponse<SubjectResponseDto>>()
                .ReverseMap();

            CreateMap<SubjectRequestDto, Subject>();

            CreateMap<Subject, SubjectSelectResponseDto>()
                .ForMember(s => s.SubjectId, s => s.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
