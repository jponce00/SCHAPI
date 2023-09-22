namespace SCHAPI.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITeacherRepository Teacher { get; }

        IStudentRepository Student { get; }

        ISubjectRepository Subject { get; }

        IClassroomRepository Classroom { get; }

        ILessonRepository Lesson { get; }

        ILessonStudentRepository LessonStudent { get; }

        void SaveChanges();

        Task SaveChangesAsync();

        Task<bool> CreateDatabaseAsync();
    }
}
