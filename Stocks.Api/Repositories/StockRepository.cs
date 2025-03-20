using System.Linq.Dynamic.Core;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Stocks.Api.DTOs.Stock;

namespace Stocks.Api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock is null)
                return null;
            _context.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<IEnumerable<StockDTO>> GetAllAsync([FromQuery] StockQueryObject query)
        {
            var res = _context.Stocks.AsQueryable().StockDTOFromStock();

            res = res.Where(s => s.CompanyName.Contains(query.CompanyName) || string.IsNullOrWhiteSpace(query.CompanyName));
            int skipCount = (query.Page - 1) * query.PageSize;
            query.PageSize = Math.Min(query.PageSize, 50);
            res = res.Skip(skipCount).Take(query.PageSize);

            var ordering = query.OrderDescending ? " descending" : string.Empty;
            query.OrderBy = query.OrderBy?.Trim();

            if (!string.IsNullOrWhiteSpace(query.OrderBy)
                && typeof(StockDTO).GetProperty(query.OrderBy, BindingFlags.IgnoreCase
             | BindingFlags.Public | BindingFlags.Instance) is not null)
                res = res.OrderBy(query.OrderBy + ordering);
            else
                res = (query.OrderDescending) ? res.OrderByDescending(s => s.Id) : res.OrderBy(s => s.Id);

            return await res.ToListAsync();
        }


        public async Task<Stock> GetByIdAsync(int id)
            => await _context.Stocks.Include(s => s.Comments)
            .ThenInclude(c => c.AppUser)
            .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<PortfolioStockDTO> GetStockIdBySymbol(string symbol)
        {
            var stock = await _context.Stocks.Select(s => new PortfolioStockDTO { StockId = s.Id, Symbol = s.Symbol })
                .FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
            return stock;
        }

        public async Task<bool> StockExist(int id)
            => await _context.Stocks.AnyAsync(s => s.Id == id);

        public async Task<Stock> UpdateAsync(int id, UpdateStockDTO dto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock is null)
                return null;
            UpdateStockData(dto, stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        private void UpdateStockData(UpdateStockDTO dto, Stock stock)
        {
            stock.Industry = dto.Industry ?? stock.Industry;
            stock.Purchase = dto.Purchase ?? stock.Purchase;
            stock.MarketCap = dto.MarketCap ?? stock.MarketCap;
            stock.Symbol = dto.Symbol ?? stock.Symbol;
            stock.CompanyName = dto.CompanyName ?? stock.CompanyName;
            stock.LastDiv = dto.LastDiv ?? stock.LastDiv;
        }
    }
}
