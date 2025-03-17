

using Stocks.Api.DTOs.Stocks;

namespace Stocks.Api.Mapper
{
    [Mapper]
    public partial class StockMapper : IStockMapper
    {
        public partial Stock StockFromCreateStockDTO(CreateStockDTO dto);

    }
}
