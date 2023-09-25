namespace SCHAPI.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string LessonCode { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public int ClassroomId { get; set; }

        public Classroom Classroom { get; set; }

        public ICollection<LessonStudent> Students { get; set; } = new HashSet<LessonStudent>();
    }
}
