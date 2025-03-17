namespace Stocks.Api.DTOs.Stocks
{
    public class UpdateStockDTO
    {
        [StringLength(255)]
        public string Symbol { get; set; }

        [StringLength(255, ErrorMessage = "Company max length 255")]
        public string CompanyName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? LastDiv { get; set; }

        [StringLength(255)]
        public string Industry { get; set; }

        public long? MarketCap { get; set; }
    }
}
