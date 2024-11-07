namespace Application.Dto.Pagination
{
    public class PaginationResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; } = new List<T>();
        public PaginationResponse(IQueryable<T> data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;

            TotalRecords = data.Count();

            TotalPages = (int)Math.Ceiling((decimal)TotalRecords / pageSize);

            Data = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
