using Stocks.Api.DTOs.Comments;

namespace Stocks.Api.Mapper
{
    //[Mapper]
    public static class CommentMapper
    {
        //[MapProperty(nameof(comment.Stock.CompanyName), nameof(CommentDTO.StockCompany))]
        public static CommentDTO CommentDTOFromComment(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockCompany = comment.Stock.CompanyName,
                StockId = comment.StockId,
                Title = comment.Title
            };
        }

        public static IQueryable<CommentDTO> CommentDTOFromComment(this IQueryable<Comment> comment)
        {
            return comment.Select(c => new CommentDTO
            {
                Content = c.Content,
                CreatedOn = c.CreatedOn,
                Id = c.Id,
                StockCompany = c.Stock.CompanyName,
                StockId = c.StockId,
                Title = c.Title
            });
        }

        public static Comment CommentFromCreateCommentDTO(this CreateCommentDTO dto)
        {
            return new Comment
            {
                Content = dto.Content,
                Title = dto.Title,
                StockId = dto.StockId
            };
        }
    }
}
