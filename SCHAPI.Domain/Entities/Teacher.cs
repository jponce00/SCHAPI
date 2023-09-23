namespace SCHAPI.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public string TeacherCode { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
    }
}
