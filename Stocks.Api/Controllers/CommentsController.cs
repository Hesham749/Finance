using Microsoft.AspNetCore.Mvc;
using Stocks.Api.DTOs.Comments;
using Stocks.Api.Mapper;

namespace Stocks.Api.Controllers
{
    [Route("api/Comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _StockRepo;

        public CommentsController(ICommentRepository commentRepository, IStockRepository stockRepo)
        {
            _commentRepo = commentRepository;
            _StockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject query)
        {
            var res = await _commentRepo.GetAllAsync(query);
            if (!res.Any()) return NotFound("No Comments Found");

            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var commentDto = await _commentRepo.GetByIdAsync(id);
            if (commentDto is null) return NotFound("No Comments Found");

            return Ok(commentDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCommentDTO dto)
        {
            var existStock = await _StockRepo.StockExist(dto.StockId);
            if (!existStock) return BadRequest("Stock doesn't exists");
            var comment = await _commentRepo.CreateAsync(dto.CommentFromCreateCommentDTO());
            return CreatedAtAction(nameof(Get), new { comment.Id }, comment);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment is null) return NotFound("No Comments Found");

            return Ok(comment);
        }
    }
}
