namespace WebTutorial.Helper
{
    public class QueryObject
    {
        public string? Sybol { get; set; } = null;
        public string? Company { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
