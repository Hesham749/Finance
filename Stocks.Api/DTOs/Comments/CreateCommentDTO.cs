namespace Stocks.Api.DTOs.Comments
{
    public class CreateCommentDTO
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required, MaxLength(255)]
        public string Content { get; set; }

        public int StockId { get; set; }

    }
}
