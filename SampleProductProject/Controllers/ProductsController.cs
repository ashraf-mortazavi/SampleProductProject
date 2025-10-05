using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProductProject.Commands;

namespace SampleProductProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] ImportUserProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Errors.Count > 0)
                return BadRequest(result.Errors);

            return Ok(result.Success);
        }
    }
}
