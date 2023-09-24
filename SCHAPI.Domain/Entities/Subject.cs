namespace SCHAPI.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string SubjectCode { get; set; }

        public string Name { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
    }
}
