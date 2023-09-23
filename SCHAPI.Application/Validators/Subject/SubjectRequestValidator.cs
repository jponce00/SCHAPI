using FluentValidation;
using SCHAPI.Application.Dtos.Subject.Request;

namespace SCHAPI.Application.Validators.Subject
{
    public class SubjectRequestValidator : AbstractValidator<SubjectRequestDto>
    {
        public SubjectRequestValidator()
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío.");
        }
    }
}
