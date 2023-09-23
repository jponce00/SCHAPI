using AutoMapper;
using SCHAPI.Application.Dtos.Lesson.Request;
using SCHAPI.Application.Dtos.Lesson.Response;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Mappers
{
    public class LessonMappingsProfile : Profile
    {
        public LessonMappingsProfile()
        {
            CreateMap<Lesson, LessonResponseDto>()
                .ForMember(l => l.LessonId, l => l.MapFrom(y => y.Id))
                .ForMember(l => l.StartHour, l => l.MapFrom(y => y.Schedule.StartHour.ToString()))
                .ForMember(l => l.SubjectName, l => l.MapFrom(y => y.Subject.Name))
                .ForMember(l => l.TeacherName, l => l.MapFrom(y => y.Teacher.Name))
                .ForMember(l => l.ClassroomName, l => l.MapFrom(y => y.Classroom.Name))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Lesson>, BaseEntityResponse<LessonResponseDto>>()
                .ReverseMap();

            CreateMap<LessonRequestDto, Lesson>();
        }
    }
}
