using FluentValidation;
using SCHAPI.Application.Dtos.Teacher.Request;

namespace SCHAPI.Application.Validators.Teacher
{
    public class TeacherValidator : AbstractValidator<TeacherRequestDto>
    {
        public TeacherValidator()
        {
            RuleFor(t => t.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Nombre no puede ser vacío.");

            RuleFor(t => t.Phone)
                .NotNull().WithMessage("El campo Teléfono no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Teléfono no puede ser vacío.");

            RuleFor(t => t.Email)
                .NotNull().WithMessage("El campo Correo Electrónico no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Correo Electrónico no puede ser vacío.");
        }
    }
}
