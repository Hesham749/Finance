namespace Finance.Api.Helpers
{
    public class QueryObject
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public bool OrderDescending { get; set; }
        public string OrderBy { get; set; }
    }
}
