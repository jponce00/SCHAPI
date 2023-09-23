using FluentValidation;
using SCHAPI.Application.Dtos.Classroom.Request;

namespace SCHAPI.Application.Validators.Classroom
{
    public class ClassroomRequestValidator : AbstractValidator<ClassroomRequestDto>
    {
        public ClassroomRequestValidator()
        {
            RuleFor(cl => cl.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío.");
        }
    }
}
