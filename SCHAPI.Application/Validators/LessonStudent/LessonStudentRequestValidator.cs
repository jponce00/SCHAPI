using FluentValidation;
using SCHAPI.Application.Dtos.LessonStudent.Request;
using SCHAPI.Utilities.Static;

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

            RuleFor(t => t.State)
                .InclusiveBetween((int)StateTypes.Inactive, (int)StateTypes.Active)
                    .WithMessage("Debe seleccionar un estado correcto.");
        }
    }
}
