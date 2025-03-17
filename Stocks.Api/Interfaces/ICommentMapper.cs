using Stocks.Api.DTOs.Comments;

namespace Stocks.Api.Interfaces
{
    public interface ICommentMapper
    {
        CommentDTO CommentDtoFromComment(Comment comment);
    }
}
