using FluentValidation;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Application.Validators.Teacher
{
    public class TeacherEntityValidator : AbstractValidator<Domain.Entities.Teacher>
    {
        private readonly SCHAPIContext _context;

        public TeacherEntityValidator(SCHAPIContext context)
        {
            _context = context;

            RuleFor(t => t.Name)
                .Must((t, v) => IsUnique(t.Id, v)).WithMessage("El Nombre ya se encuentra en uso.");

            RuleFor(t => t.Phone)
                .Must((t, v) => IsUnique(t.Id, v)).WithMessage("El Teléfono ya se encuentra en uso.");

            RuleFor(t => t.Email)
                .Must((t, v) => IsUnique(t.Id, v)).WithMessage("El Correo Electrónico ya se encuentra en uso.");
        }

        private bool IsUnique(int id, string value)
        {
            var model = _context.Teachers
                .Where(t => t.AuditDeleteUser == null && t.AuditDeleteDate == null && t.Id != id)
                .FirstOrDefault(t =>
                    t.Name.Equals(value) ||
                    t.Phone.Equals(value) ||
                    t.Email.Equals(value));

            return model == null;
        }
    }
}
