namespace Stocks.Api.Interfaces
{
    public interface IStockMapper
    {
          Stock StockFromCreateStockDTO(CreateStockDTO dto);
    }
}
