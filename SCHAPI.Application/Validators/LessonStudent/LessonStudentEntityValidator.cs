using FluentValidation;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Application.Validators.LessonStudent
{
    public class LessonStudentEntityValidator : AbstractValidator<Domain.Entities.LessonStudent>
    {
        private readonly SCHAPIContext _context;
        private const int MAX_LESSONS_PER_STUDENT = 5;

        public LessonStudentEntityValidator(SCHAPIContext context)
        {
            _context = context;

            RuleFor(ls => ls.LessonId)
                .Must(ls => _context.Lessons.Any(le => le.Id == ls))
                    .WithMessage("La clase seleccionada no está disponible.");

            RuleFor(ls => ls.StudentId)
                .Must(st => _context.Students.Any(stu => stu.Id == st))
                    .WithMessage("El estudiante seleccionado no está disponible.")
                .Must(st => ValidateLessonsOfStudent(st))
                    .WithMessage($"Un estudiante solo puede matricular un máximo de {MAX_LESSONS_PER_STUDENT} clases.");
        }

        private bool ValidateLessonsOfStudent(int studentId)
        {
            var result = _context.LessonStudents.Count(ls => ls.StudentId == studentId);

            return result < MAX_LESSONS_PER_STUDENT;
        }
    }
}
