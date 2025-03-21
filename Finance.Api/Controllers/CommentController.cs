using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Finance.Api.DTOs.Comments;
using Finance.Api.Extensions;

namespace Finance.Api.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject query)
        {
            var res = await _commentRepo.GetAllAsync(query);
            if (!res.Any()) return NotFound("No Comments Found");

            return Ok(res);
        }

        [AllowAnonymous]
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
            var comment = dto.CommentFromCreateCommentDTO(stockId);
            var userId = User.GetUserId();
            comment.AppUserId = userId;
            comment.AppUser = new AppUser { UserName = User.GetUserName() };
            var commentDTO = await _commentRepo.CreateAsync(comment);
            if (commentDTO == null) return BadRequest("Stock doesn't exists");
            return CreatedAtAction(nameof(Get), new { commentDTO.Id }, commentDTO);
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
