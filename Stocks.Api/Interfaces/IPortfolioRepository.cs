namespace Stocks.Api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Stock>> GetUserPortfolio(string userId);
        Task<Portfolio> CreateAsync(string userId, int stockId);
    }
}
