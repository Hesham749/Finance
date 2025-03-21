using Finance.Api.DTOs.Stock;

namespace Finance.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockDTO>> GetAllAsync(StockQueryObject query);
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<PortfolioStockDTO> GetStockIdBySymbol(string symbol);
        Task<Stock> UpdateAsync(int id, UpdateStockDTO dto);
        Task<Stock> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}
