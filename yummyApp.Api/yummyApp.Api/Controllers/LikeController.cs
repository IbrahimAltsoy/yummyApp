using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using yummyApp.Application.Features.Likes.Commands;
using yummyApp.Application.Features.Likes.Queries.GetAll;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        readonly IMediator _mediator;

        public LikeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllLikeQueryRequest request)
        {
            List<GetAllLikeQueryResponse> response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateLikecommandRequest request)
        {
            CreateLikecommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
