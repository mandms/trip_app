using System.ComponentModel;

namespace Domain.Filters
{
    [DisplayName("Pagination response")]
    public record PaginationResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; } = new List<T>();

        private PaginationResponse(int pageNumber, int pageSize, int totalPages, int totalRecords, List<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            Data = data;
        }

        public static PaginationResponse<T> CreatepagedResponse(IQueryable<T> data, int pageNumber, int pageSize)
        {
            var totalRecords = data.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

            var items = data.Skip((pageNumber- 1)*pageSize).Take(pageSize).ToList();

            return new(pageNumber, pageSize, totalPages, totalRecords, items);
        }
    }
}
