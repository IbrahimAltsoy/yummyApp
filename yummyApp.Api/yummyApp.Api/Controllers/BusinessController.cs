using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Features.Businesses.Commands.CreateBusiness;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        readonly IMediator _mediator;

        public BusinessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBusinessCommand command)
        {
            var response =await _mediator.Send(command);
            var x = 5;
            return Ok(response);
        }
    }
}
