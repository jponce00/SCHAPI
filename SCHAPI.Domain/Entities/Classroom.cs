namespace SCHAPI.Domain.Entities
{
    public class Classroom : BaseEntity
    {
        public string ClassroomCode { get; set; }

        public string Name { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
    }
}
