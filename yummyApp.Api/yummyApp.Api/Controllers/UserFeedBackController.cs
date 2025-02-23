using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Features.UserFeedBacks.Commands.Create;
using yummyApp.Application.Features.UserFeedBacks.Commands.Delete;
using yummyApp.Application.Features.UserFeedBacks.Commands.Update;
using yummyApp.Application.Features.UserFeedBacks.Queries.Get;
using yummyApp.Application.Features.UserFeedBacks.Queries.GetById;
using yummyApp.Application.Features.UserFeedBacks.Queries.GetIsAddressed;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFeedBackController : ControllerBase
    {
        readonly IMediator _mediator;

        public UserFeedBackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdUserFeedBackQueryRequest request)
        {
            GetByIdUserFeedBackQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus([FromQuery] GetIsAddressedUserFeedbackQueryRequest request)
        {
            GetAllUserFeedBackQueryResult response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUserFeedBackQueryRequest request)
        {
            GetAllUserFeedBackQueryResult response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserFeedBackCommandRequest request)
        {
            CreateUserFeedBackCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserFeedBackCommandRequest request)
        {
            UpdateUserFeedBackCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserFeedBackCommandRequest request)
        {
            DeleteUserFeedBackCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
