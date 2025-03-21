namespace Finance.Api.DTOs.Comments
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? StockId { get; set; }

        public string StockCompany { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
