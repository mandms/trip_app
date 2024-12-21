namespace Domain.Filters
{
    public class FilterParamsWithDate : FilterParams
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
