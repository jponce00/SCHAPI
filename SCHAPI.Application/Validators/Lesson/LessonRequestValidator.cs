using FluentValidation;
using SCHAPI.Application.Dtos.Lesson.Request;

namespace SCHAPI.Application.Validators.Lesson
{
    public class LessonRequestValidator : AbstractValidator<LessonRequestDto>
    {
        public LessonRequestValidator()
        {
            RuleFor(l => l.ScheduleId)
                .NotNull().WithMessage("Debe seleccionar un horario correcto.");

            RuleFor(l => l.TeacherId)
                .NotNull().WithMessage("Debe seleccionar un maestro.");

            RuleFor(l => l.SubjectId)
                .NotNull().WithMessage("Debe seleccionar una materia.");

            RuleFor(l => l.ClassroomId)
                .NotNull().WithMessage("Debe seleccionar un aula de clases.");
        }
    }
}
