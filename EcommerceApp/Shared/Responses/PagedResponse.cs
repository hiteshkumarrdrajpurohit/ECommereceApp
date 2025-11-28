namespace EcommerceApp.Shared.Responses
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = Array.Empty<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;
    }
}
