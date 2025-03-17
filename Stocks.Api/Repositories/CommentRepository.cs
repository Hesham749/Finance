
using System.Reflection;
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

        public Task<Comment> CreateAsync(Comment Comment)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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

            var mappedComments = comments.Select(c => c.CommentDtoFromComment());

            var skipCount = (query.Page - 1) * query.PageSize;
            var pagedComments = mappedComments.Skip(skipCount).Take(query.PageSize);

            return await pagedComments.ToListAsync();
        }

        public Task<Comment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
