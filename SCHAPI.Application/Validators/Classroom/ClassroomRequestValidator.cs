using FluentValidation;
using SCHAPI.Application.Dtos.Classroom.Request;
using SCHAPI.Utilities.Static;

namespace SCHAPI.Application.Validators.Classroom
{
    public class ClassroomRequestValidator : AbstractValidator<ClassroomRequestDto>
    {
        public ClassroomRequestValidator()
        {
            RuleFor(cl => cl.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío.");

            RuleFor(t => t.State)
                .InclusiveBetween((int)StateTypes.Inactive, (int)StateTypes.Active)
                    .WithMessage("Debe seleccionar un estado correcto.");
        }
    }
}
