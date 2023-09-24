using FluentValidation;
using SCHAPI.Infrastructure.Persistences.Contexts;

namespace SCHAPI.Application.Validators.Lesson
{
    public class LessonEntityValidator : AbstractValidator<Domain.Entities.Lesson>
    {
        private readonly SCHAPIContext _context;

        public LessonEntityValidator(SCHAPIContext context)
        {
            _context = context;

            RuleFor(l => l.ScheduleId)
                .Must(s => ValidateSchedule(s)).WithMessage("El horario seleccionado no está disponible.");

            RuleFor(l => l.TeacherId)
                .Must((l, t) => ValidateTeacherLessons(l.Id, t)).WithMessage("Un maestro solo puede impartir de una a cuatro clases.")
                .Must((l, t) => ValidateTeacherSchedule(l.Id, t, l.ScheduleId)).WithMessage("El maestro seleccionado ya tiene una clase asignada a esta hora.");

            RuleFor(l => l.ClassroomId)
                .Must((l, c) => ValidateClassroomSchedule(l.Id, c, l.ScheduleId)).WithMessage("Ya existe una clase asignada en esta aula a esta hora.");
        }

        private bool ValidateSchedule(int scheduleId)
        {
            int result = _context.Schedules
                .Count(s => s.Id == scheduleId);

            return result != 0;
        }

        private bool ValidateTeacherLessons(int lessonId, int teacherId)
        {
            var result = _context.Lessons
                .Where(l => l.AuditDeleteUser == null && l.AuditDeleteDate == null && l.Id != lessonId)
                .Count(l => l.TeacherId == teacherId);

            return result <= 4;
        }

        private bool ValidateTeacherSchedule(int lessonId, int teacherId, int scheduleId)
        {
            var result = _context.Lessons
                .Where(l => l.AuditDeleteUser == null && l.AuditDeleteDate == null && l.Id != lessonId)
                .Count(l => l.TeacherId == teacherId && l.ScheduleId == scheduleId);

            return result == 0;
        }

        private bool ValidateClassroomSchedule(int lessonId, int classroomId, int scheduleId)
        {
            var result = _context.Lessons
                .Where(l => l.AuditDeleteUser == null && l.AuditDeleteDate == null && l.Id != lessonId)
                .Count(l => l.ClassroomId == classroomId && l.ScheduleId == scheduleId);

            return result == 0;
        }
    }
}
