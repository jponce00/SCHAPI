namespace SCHAPI.Domain.Interfaces
{
    public interface IDeleteAuditable
    {
        public int? AuditDeleteUser { get; set; }

        public DateTime? AuditDeleteDate { get; set; }
    }
}
