namespace SCHAPI.Domain.Entities
{
    public class LessonStudent : BaseEntity
    {
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
