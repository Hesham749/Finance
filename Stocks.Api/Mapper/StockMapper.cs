using Stocks.Api.DTOs.Comments;
using Stocks.Api.DTOs.Stock;

namespace Stocks.Api.Mapper
{
    //[Mapper]
    //public partial class StockMapper : IStockMapper
    //{
    //    public partial Stock StockFromCreateStockDTO(CreateStockDTO dto);

    //    public partial StockDTO StockDTOFromStock(Stock stock);

    //}

    public static class StockMapper
    {
        public static IQueryable<StockDTO> StockDTOFromStock(this IQueryable<Stock> stock)
        {
            return stock.Select(s => new StockDTO
            {
                Id = s.Id,
                CompanyName = s.CompanyName,
                Industry = s.Industry,
                LastDiv = s.LastDiv,
                Symbol = s.Symbol,
                MarketCap = s.MarketCap,
                Purchase = s.Purchase,
                Comments = s.Comments.Select(c => new CommentDTO
                {
                    Content = c.Content,
                    CreatedBy = c.AppUser.UserName,
                    Id = c.Id,
                    Title = c.Title,
                    CreatedOn = c.CreatedOn,
                    StockCompany = c.Stock.CompanyName,
                    StockId = c.StockId
                }).ToList()
            });
        }

        public static Stock StockFromCreateStockDTO(this CreateStockDTO createStockDTO)
        {
            return new Stock
            {
                Industry = createStockDTO.Industry,
                CompanyName = createStockDTO.CompanyName,
                Symbol = createStockDTO.Symbol,
                Purchase = createStockDTO.Purchase,
                LastDiv = createStockDTO.LastDiv,
                MarketCap = createStockDTO.MarketCap
            };
        }
    }
}
