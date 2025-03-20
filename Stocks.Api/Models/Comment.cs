namespace Stocks.Api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required, MaxLength(255)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }

        public Stock Stock { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; }
    }
}
