namespace Stocks.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync(StockQueryObject query);
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock> UpdateAsync(int id, UpdateStockDTO dto);
        Task<Stock> DeleteAsync(int id);
    }
}
