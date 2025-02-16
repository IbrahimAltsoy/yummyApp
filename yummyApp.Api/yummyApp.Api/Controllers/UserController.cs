using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.BackGroundJobs;
using yummyApp.Application.Features.Users.Commands.Create;
using yummyApp.Application.Features.Users.Commands.Delete;
using yummyApp.Application.Features.Users.Commands.Update;
using yummyApp.Application.Features.Users.Queries.GetAll;
using yummyApp.Application.Features.Users.Queries.GetUserById;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUserQueryRequest request)
        {
            GetAllUserQueryResponse response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] GetUserByIdQueryRequest request)
        {
            GetUserByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommandRequest request)
        {
            UpdateUserCommandResponse response = await _mediator.Send(request);

            return Ok("Güncelleme başarılı bir şekilde yapılmıştır.");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteUserCommandRequest request)
        {
            DeleteUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
