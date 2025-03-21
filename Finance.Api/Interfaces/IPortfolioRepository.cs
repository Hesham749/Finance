using Finance.Api.DTOs.Stock;

namespace Finance.Api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<StockDTO>> GetUserPortfolio(string userId);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeleteAsync(Portfolio portfolio);
        Task<bool> PortfolioExist(Portfolio portfolio);
    }
}
