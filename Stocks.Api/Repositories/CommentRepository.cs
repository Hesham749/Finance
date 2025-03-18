using Stocks.Api.DTOs.Comments;
using Stocks.Api.Mapper;

namespace Stocks.Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        //private readonly ICommentMapper _commentMapper;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentDTO> CreateAsync(Comment Comment)
        {
            //var 
            await _context.AddAsync(Comment);
            await _context.SaveChangesAsync();
            return Comment.CommentDTOFromComment();
        }

        public async Task<CommentDTO> DeleteAsync(int id)
        {
            var res = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (res is null) return null;
            _context.Remove(res);
            await _context.SaveChangesAsync();
            return res.CommentDTOFromComment();
        }

        public async Task<IEnumerable<CommentDTO>> GetAllAsync(CommentQueryObject query)
        {
            var comments = _context.Comments.Include(c => c.Stock).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                comments = comments.Where(c => c.Stock.CompanyName.Contains(
                    query.CompanyName,
                    StringComparison.OrdinalIgnoreCase));
            }



            var mappedComments = comments.Select(c => c.CommentDTOFromComment());

            var skipCount = (query.Page - 1) * query.PageSize;
            var pagedComments = mappedComments.Skip(skipCount).Take(query.PageSize);

            return await pagedComments.ToListAsync();
        }

        public async Task<CommentDTO> GetByIdAsync(int id)
        {
            var res = await _context.Comments.Include(c => c.Stock)
                .Select(c => c.CommentDTOFromComment())
                .FirstOrDefaultAsync(c => c.Id == id);
            return res;
        }

        public Task<CommentDTO> UpdateAsync(int id, UpdateCommentDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
