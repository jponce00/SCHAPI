using FluentValidation;
using SCHAPI.Application.Dtos.LessonStudent.Request;

namespace SCHAPI.Application.Validators.LessonStudent
{
    public class LessonStudentRequestValidator : AbstractValidator<LessonStudentRequestDto>
    {
        public LessonStudentRequestValidator()
        {
            RuleFor(ls => ls.LessonId)
                .NotNull().WithMessage("Debe seleccionar una clase.");

            RuleFor(ls => ls.StudentId)
                .NotNull().WithMessage("Debe seleccionar un alumno correcto.");
        }
    }
}
