﻿namespace Domain.Filters
{
    public class FilterParams
    {
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
