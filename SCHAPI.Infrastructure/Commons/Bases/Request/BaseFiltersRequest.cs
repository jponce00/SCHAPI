namespace SCHAPI.Infrastructure.Commons.Bases.Request
{
    public class BaseFiltersRequest : BasePaginationRequest
    {
        public int? NumFilter { get; set; } = null;

        public string? TextFilter { get; set; } = null;
    }
}
