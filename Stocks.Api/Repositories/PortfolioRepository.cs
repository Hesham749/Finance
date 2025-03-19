
namespace Stocks.Api.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(string userId, int stockId)
        {
            var HasPortfolio = await _context.Portfolios.AnyAsync(p => p.StockId == stockId && p.AppUserId == userId);
            if (HasPortfolio) return null;
            var createdPortfolio = new Portfolio { AppUserId = userId, StockId = stockId };
            await _context.AddAsync(createdPortfolio);
            await _context.SaveChangesAsync();
            return createdPortfolio;
        }

        public async Task<IEnumerable<Stock>> GetUserPortfolio(string userId)
        {
            return await _context.Portfolios
                 .Where(p => p.AppUserId == userId)
                 .Select(p => new Stock
                 {
                     Id = p.StockId,
                     CompanyName = p.Stock.CompanyName,
                     Industry = p.Stock.Industry,
                     LastDiv = p.Stock.LastDiv,
                     Symbol = p.Stock.Symbol,
                     MarketCap = p.Stock.MarketCap,
                 }).ToListAsync();
        }
    }
}
