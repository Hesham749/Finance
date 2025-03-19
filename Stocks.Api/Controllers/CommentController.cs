using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocks.Api.DTOs.Comments;

namespace Stocks.Api.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _StockRepo;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepo)
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

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var commentDto = await _commentRepo.GetByIdAsync(id);
            if (commentDto is null) return NotFound("No Comments Found");

            return Ok(commentDto);
        }

        [HttpPost("{stockId:int}")]
        public async Task<ActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDTO dto)
        {
            var comment = await _commentRepo.CreateAsync(dto.CommentFromCreateCommentDTO(stockId));
            if (comment == null) return BadRequest("Stock doesn't exists");
            return CreatedAtAction(nameof(Get), new { comment.Id }, comment);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDTO dto)
        {
            var res = await _commentRepo.UpdateAsync(id, dto);
            if (res is null) return NotFound($"comment with id {id} not found");
            return Ok(res);
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
