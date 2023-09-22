using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;

namespace SCHAPI.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SCHAPIContext _context;

        public ITeacherRepository Teacher { get; private set; }

        public IStudentRepository Student { get; private set; }

        public ISubjectRepository Subject { get; private set; }

        public IClassroomRepository Classroom { get; private set; }

        public ILessonRepository Lesson { get; private set; }

        public ILessonStudentRepository LessonStudent { get; private set; }

        public UnitOfWork(SCHAPIContext context)
        {
            _context = context;

            Teacher = new TeacherRepository(_context);
            Student = new StudentRepository(_context);
            Subject = new SubjectRepository(_context);
            Classroom = new ClassroomRepository(_context);
            Lesson = new LessonRepository(_context);
            LessonStudent = new LessonStudentRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CreateDatabaseAsync()
        {
            return await _context.Database.EnsureCreatedAsync();
        }
    }
}
