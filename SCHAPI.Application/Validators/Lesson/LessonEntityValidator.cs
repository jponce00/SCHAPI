using FluentValidation;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Application.Validators.Lesson
{
    public class LessonEntityValidator : AbstractValidator<Domain.Entities.Lesson>
    {
        private readonly SCHAPIContext _context;
        private const int MAX_SUBJECTS_PER_TEACHER = 4;

        public LessonEntityValidator(SCHAPIContext context)
        {
            _context = context;

            RuleFor(l => l.ScheduleId)
                .Must(s => _context.Schedules.Any(sc => sc.Id == s))
                    .WithMessage("El horario seleccionado no está disponible.");

            RuleFor(l => l.TeacherId)
                .Must(t => _context.Teachers.Any(te => te.Id == t))
                    .WithMessage("El maestro seleccionado no está disponible")
                .Must((l, t) => ValidateTeacherLessons(l.Id, t))
                    .WithMessage($"Un maestro solo puede impartir un máximo de {MAX_SUBJECTS_PER_TEACHER} clases.")
                .Must((l, t) => ValidateTeacherSchedule(l.Id, t, l.ScheduleId))
                    .WithMessage("El maestro seleccionado ya tiene una clase asignada a esta hora.");

            RuleFor(l => l.SubjectId)
                .Must(s => _context.Subjects.Any(su => su.Id == s))
                    .WithMessage("La materia seleccionada no está disponible.");

            RuleFor(l => l.ClassroomId)
                .Must(c => _context.Classrooms.Any(cl => cl.Id == c))
                    .WithMessage("El aula seleccionada no está disponible.")
                .Must((l, c) => ValidateClassroomSchedule(l.Id, c, l.ScheduleId))
                    .WithMessage("Ya existe una clase asignada en esta aula a esta hora.");
        }

        private bool ValidateTeacherLessons(int lessonId, int teacherId)
        {
            var result = _context.Lessons
                .Where(l => l.Id != lessonId)
                .Count(l => l.TeacherId == teacherId);

            return result <= MAX_SUBJECTS_PER_TEACHER;
        }

        private bool ValidateTeacherSchedule(int lessonId, int teacherId, int scheduleId)
        {
            return !_context.Lessons
                .Where(l => l.Id != lessonId)
                .Any(l => l.TeacherId == teacherId && l.ScheduleId == scheduleId);
        }

        private bool ValidateClassroomSchedule(int lessonId, int classroomId, int scheduleId)
        {
            return !_context.Lessons
                .Where(l => l.Id != lessonId)
                .Any(l => l.ClassroomId == classroomId && l.ScheduleId == scheduleId);
        }
    }
}
