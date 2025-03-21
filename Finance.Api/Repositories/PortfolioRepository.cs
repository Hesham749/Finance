using Finance.Api.DTOs.Comments;
using Finance.Api.DTOs.Stock;

namespace Finance.Api.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {

            try
            {
                await _context.AddAsync(portfolio);
                await _context.SaveChangesAsync();
                return portfolio;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Portfolio> DeleteAsync(Portfolio portfolio)
        {
            _context.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<IEnumerable<StockDTO>> GetUserPortfolio(string userId)
        {
            return await _context.Portfolios
                 .Where(p => p.AppUserId == userId)
                 //.Include(p => p.Stock)
                 //.ThenInclude(s => s.Comments)
                 //.ThenInclude(c => c.AppUser)
                 //.Include(c => c.Stock)
                 .Select(p => new StockDTO
                 {
                     Id = p.StockId,
                     CompanyName = p.Stock.CompanyName,
                     Industry = p.Stock.Industry,
                     LastDiv = p.Stock.LastDiv,
                     Symbol = p.Stock.Symbol,
                     MarketCap = p.Stock.MarketCap,
                     Purchase = p.Stock.Purchase,
                     Comments = p.Stock.Comments.Select(c => new CommentDTO
                     {
                         Content = c.Content,
                         CreatedBy = c.AppUser.UserName,
                         Id = c.Id,
                         Title = c.Title,
                         CreatedOn = c.CreatedOn,
                         StockCompany = c.Stock.CompanyName,
                         StockId = c.StockId
                     }).ToList(),
                 }).ToListAsync();
        }

        public async Task<bool> PortfolioExist(Portfolio portfolio)
        {
            return await _context.Portfolios
                .AnyAsync(p => p.StockId == portfolio.StockId && p.AppUserId == portfolio.AppUserId);
        }
    }
}
