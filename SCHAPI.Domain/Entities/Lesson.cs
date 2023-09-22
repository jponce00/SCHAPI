namespace SCHAPI.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int StartHour { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public int ClassroomId { get; set; }

        public Classroom Classroom { get; set; }
    }
}
