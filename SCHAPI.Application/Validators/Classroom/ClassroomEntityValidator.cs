using FluentValidation;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Application.Validators.Classroom
{
    public class ClassroomEntityValidator : AbstractValidator<Domain.Entities.Classroom>
    {
        private readonly SCHAPIContext _context;

        public ClassroomEntityValidator(SCHAPIContext context)
        {
            _context = context;

            RuleFor(cl => cl.Name)
                .Must((cl, v) => IsUnique(cl.Id, v)).WithMessage("El Nombre ya se encuentra en uso.");
        }

        private bool IsUnique(int id, string value)
        {
            var model = _context.Classrooms
                .Where(cl => cl.AuditDeleteUser == null && cl.AuditDeleteDate == null && cl.Id != id)
                .FirstOrDefault(cl =>
                    cl.Name.Equals(value));

            return model == null;
        }
    }
}
