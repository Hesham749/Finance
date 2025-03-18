using Microsoft.AspNetCore.Mvc;

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

        public async Task<IEnumerable<Stock>> GetAllAsync([FromQuery] StockQueryObject query)
        {
            var res = _context.Stocks.Include(s => s.Comments).AsQueryable();
            res = res.Where(s => s.CompanyName.Contains(query.CompanyName) || string.IsNullOrWhiteSpace(query.CompanyName));
            int skipCount = (query.Page - 1) * query.PageSize;
            query.PageSize = Math.Min(query.PageSize, 50);
            res = res.Skip(skipCount).Take(query.PageSize);
            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            //todo
            //&& typeof(Stock).GetProperty(query.OrderBy?.Trim(), BindingFlags.IgnoreCase) is not null)
            {
                res = query.OrderDescending ? res.OrderByDescending(s => EF.Property<object>(s, query.OrderBy))
                    : res.OrderBy(s => EF.Property<object>(s, query.OrderBy));
            }

            return await res.ToListAsync();
        }


        public async Task<Stock> GetByIdAsync(int id)
            => await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);

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
