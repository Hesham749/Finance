namespace Stocks.Api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Stock>> GetUserPortfolio(string userId);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeleteAsync(Portfolio portfolio);
        Task<bool> PortfolioExist(Portfolio portfolio);
    }
}
