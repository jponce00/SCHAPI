using FluentValidation;
using SCHAPI.Application.Dtos.Subject.Request;
using SCHAPI.Utilities.Static;

namespace SCHAPI.Application.Validators.Subject
{
    public class SubjectRequestValidator : AbstractValidator<SubjectRequestDto>
    {
        public SubjectRequestValidator()
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío.");

            RuleFor(t => t.State)
                .InclusiveBetween((int)StateTypes.Inactive, (int)StateTypes.Active)
                    .WithMessage("Debe seleccionar un estado correcto.");
        }
    }
}
