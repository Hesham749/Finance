namespace Stocks.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync(QueryObject query);
        Task<Stock> GetByIdAsync(int id);
    }
}
