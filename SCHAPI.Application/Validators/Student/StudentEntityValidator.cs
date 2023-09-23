using FluentValidation;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Application.Validators.Student
{
    public class StudentEntityValidator : AbstractValidator<Domain.Entities.Student>
    {
        private readonly SCHAPIContext _context;

        public StudentEntityValidator(SCHAPIContext context)
        {
            _context = context;

            RuleFor(s => s.Name)
                .Must((s, v) => IsUnique(s.Id, v)).WithMessage("El Nombre ya se encuentra en uso.");

            RuleFor(s => s.Phone)
                .Must((s, v) => IsUnique(s.Id, v)).WithMessage("El Teléfono ya se encuentra en uso.");

            RuleFor(s => s.Email)
                .Must((s, v) => IsUnique(s.Id, v)).WithMessage("El Correo Electrónico ya se encuentra en uso.");
        }

        private bool IsUnique(int id, string value)
        {
            var model = _context.Students
                .Where(s => s.AuditDeleteUser == null && s.AuditDeleteDate == null && s.Id != id)
                .FirstOrDefault(s =>
                    s.Name.Equals(value) ||
                    s.Phone.Equals(value) ||
                    s.Email.Equals(value));

            return model == null;
        }
    }
}
