using Finance.Api.DTOs.Comments;

namespace Finance.Api.Interfaces
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
