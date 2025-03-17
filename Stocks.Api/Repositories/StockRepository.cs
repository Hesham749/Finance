using System.Reflection;
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

        public async Task<IEnumerable<Stock>> GetAllAsync([FromQuery] QueryObject query)
        {
            var res = _context.Stocks.Include(s => s.Comments).AsQueryable();
            res = res.Where(s => s.CompanyName.Contains(query.CompanyName) || string.IsNullOrWhiteSpace(query.CompanyName));
            int skipCount = (query.Page - 1) * query.PageSize;
            res = res.Skip(skipCount).Take(query.PageSize);

            if (!string.IsNullOrWhiteSpace(query.OrderBy)
                && typeof(Stock).GetProperty(query.OrderBy?.Trim()) is not null)
            {
                res = query.OrderDescending ? res.OrderByDescending(s => EF.Property<object>(s, query.OrderBy))
                    : res.OrderBy(s => EF.Property<object>(s, query.OrderBy));
            }

            return await res.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<Stock> GetByIdAsync(int id)
            => await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }
}
