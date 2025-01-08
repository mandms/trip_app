using System.Text.Json.Serialization;

namespace Domain.Filters
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortOrder
    {
        Asc,
        Desc
    }
    public class FilterParams
    {
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public SortOrder Ordering { get; set; } = SortOrder.Asc;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
