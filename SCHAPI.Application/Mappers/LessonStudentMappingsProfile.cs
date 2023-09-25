using AutoMapper;
using SCHAPI.Application.Dtos.LessonStudent.Request;
using SCHAPI.Application.Dtos.LessonStudent.Response;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Response;

namespace SCHAPI.Application.Mappers
{
    public class LessonStudentMappingsProfile : Profile
    {
        public LessonStudentMappingsProfile()
        {
            CreateMap<LessonStudent, LessonStudentResponseDto>()
                .ForMember(ls => ls.LessonStudentId, ls => ls.MapFrom(y => y.Id))
                .ForMember(ls => ls.LessonCode, ls => ls.MapFrom(y => y.Lesson.LessonCode))
                .ForMember(ls => ls.SubjectName, ls => ls.MapFrom(y => y.Lesson.Subject.Name))
                .ForMember(ls => ls.StartHour, ls => ls.MapFrom(y => y.Lesson.Schedule.StartHour.ToString()))
                .ForMember(ls => ls.StudentCode, ls => ls.MapFrom(y => y.Student.StudentCode))
                .ForMember(ls => ls.StudentName, ls => ls.MapFrom(y => y.Student.Name))
                .ForMember(ls => ls.ClassroomName, ls => ls.MapFrom(y => y.Lesson.Classroom.Name))
                .ReverseMap();

            CreateMap<BaseEntityResponse<LessonStudent>, BaseEntityResponse<LessonStudentResponseDto>>()
                .ReverseMap();

            CreateMap<LessonStudentRequestDto, LessonStudent>();
        }
    }
}
