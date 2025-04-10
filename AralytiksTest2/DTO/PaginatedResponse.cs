namespace AralytiksTest2.DTO
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
