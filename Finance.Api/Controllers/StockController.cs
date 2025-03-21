using Finance.Api.DTOs.Stock;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers
{
    [Route("api/Stock")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class StockController : ControllerBase
    {
        readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject query)
        {
            var res = await _stockRepo.GetAllAsync(query);
            if (!res.Any())
                return NotFound("No Stock Was Found!");
            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            return stock is null ? NotFound($"No stock with id {id}") : Ok(stock);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateStockDTO dto)
        {
            var stock = await _stockRepo.CreateAsync(dto.StockFromCreateStockDTO());
            return CreatedAtAction(nameof(Get), new { stock.Id }, stock);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDTO dto)
        {
            var stock = await _stockRepo.UpdateAsync(id, dto);
            if (stock is null)
                return NotFound($"No stock with id {id}");
            return Ok(stock);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);
            if (stock is null)
                return NotFound($"No stock with id {id}");
            return Ok(stock);
        }
    }
}
