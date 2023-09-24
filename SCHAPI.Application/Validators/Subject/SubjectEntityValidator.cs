using FluentValidation;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Application.Validators.Subject
{
    public class SubjectEntityValidator : AbstractValidator<Domain.Entities.Subject>
    {
        private readonly SCHAPIContext _context;

        public SubjectEntityValidator(SCHAPIContext context)
        {
            _context = context;

            RuleFor(s => s.Name)
                .Must((s, v) => IsUnique(s.Id, v)).WithMessage("El Nombre ya se encuentra en uso.");
        }

        private bool IsUnique(int id, string value)
        {
            var model = _context.Subjects
                .Where(s => s.Id != id)
                .FirstOrDefault(s =>
                    s.Name.Equals(value));

            return model == null;
        }
    }
}
