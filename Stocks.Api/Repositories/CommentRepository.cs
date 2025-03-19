using Stocks.Api.DTOs.Comments;

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
            var existStock = await _context.Stocks.FindAsync(Comment.StockId);
            if (existStock is null) return null;
            Comment.Stock = existStock;
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
            var comments = _context.Comments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                comments = comments.Where(c => c.Stock.CompanyName.Contains(
                    query.CompanyName,
                    StringComparison.OrdinalIgnoreCase));
            }

            query.PageSize = Math.Min(query.PageSize, 50);
            var skipCount = (query.Page - 1) * query.PageSize;

            var pagedComments = comments.Skip(skipCount)
                                .Take(query.PageSize)
                                 .CommentDTOFromComment();
            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            //todo
            //&& typeof(Stock).GetProperty(query.OrderBy?.Trim(), BindingFlags.IgnoreCase) is not null)
            {
                pagedComments = query.OrderDescending ? pagedComments.OrderByDescending(s => EF.Property<object>(s, query.OrderBy))
                    : pagedComments.OrderBy(s => EF.Property<object>(s, query.OrderBy));
            }

            return await pagedComments.ToListAsync();
        }

        public async Task<CommentDTO> GetByIdAsync(int id)
        {
            var comment = await _context.Comments
                .CommentDTOFromComment()
                .FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }

        public async Task<CommentDTO> UpdateAsync(int id, UpdateCommentDTO dto)
        {
            var comment = await _context.Comments.Include(c => c.Stock).FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return null;

            comment.Title = dto.Title ?? comment.Title;
            comment.Content = dto.Content ?? comment.Content;
            await _context.SaveChangesAsync();
            return comment.CommentDTOFromComment();
        }
    }
}
