namespace SCHAPI.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public TimeSpan StartHour { get; set; }

        public TimeSpan EndHour { get; set; }
    }
}
