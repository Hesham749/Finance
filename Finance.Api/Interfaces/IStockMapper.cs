using Finance.Api.DTOs.Stock;

namespace Finance.Api.Interfaces
{
    public interface IStockMapper
    {
          Stock StockFromCreateStockDTO(CreateStockDTO dto);
    }
}
