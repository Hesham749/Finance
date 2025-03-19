using Newtonsoft.Json;

namespace Stocks.Api.DTOs.Stocks
{
    public class CreateStockDTO
    {

        [Required, StringLength(255)]
        public string Symbol { get; set; }

        [Required, StringLength(255)]
        public string CompanyName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [JsonProperty("lastDiv")]
        public decimal LastDiv { get; set; }

        [Required, StringLength(255)]
        public string Industry { get; set; }

        public long MarketCap { get; set; }
    }
}
