namespace SCHAPI.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string StudentCode { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
