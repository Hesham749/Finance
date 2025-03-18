using Stocks.Api.DTOs.Comments;

namespace Stocks.Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentDTO>> GetAllAsync(CommentQueryObject query);
        Task<CommentDTO> GetByIdAsync(int id);
        Task<CommentDTO> CreateAsync(Comment Comment);
        Task<CommentDTO> UpdateAsync(int id, UpdateCommentDTO dto);
        Task<CommentDTO> DeleteAsync(int id);
    }
}
