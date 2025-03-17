

namespace Stocks.Api.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Symbol { get; set; }

        [Required, StringLength(255)]
        public string CompanyName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        [Required, StringLength(255)]
        public string Industry { get; set; }

        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = [];
    }
}
