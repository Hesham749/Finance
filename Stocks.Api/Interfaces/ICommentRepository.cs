using Stocks.Api.DTOs.Comments;

namespace Stocks.Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentDTO>> GetAllAsync(CommentQueryObject query);
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment Comment);
        //Task<Comment> UpdateAsync(int id, UpdateCommentDTO dto);
        Task<Comment> DeleteAsync(int id);
    }
}
