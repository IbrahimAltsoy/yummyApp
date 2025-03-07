using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using yummyApp.Application.Features.Businesses.Commands.CreateBusiness;
using yummyApp.Application.Features.Businesses.Commands.DeleteBusiness;
using yummyApp.Application.Features.Businesses.Commands.UpdateBusiness;
using yummyApp.Application.Features.Businesses.Queries.GetAll;
using yummyApp.Application.Features.Businesses.Queries.GetById;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace yummyApp.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        readonly IMediator _mediator;

        public BusinessController(IMediator mediator)
        {
            _mediator = mediator;
        }

    
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBusinessQueryRequest request)
        { 
            IList<GetAllBusinessQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery]GetByIdQueryRequest request)
        {
            GetByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost] 
        public async Task<IActionResult> Create(CreateBusinessCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBusinessCommand request, Guid id)
        {
            if (id == request.Id)
            {
                var response = await _mediator.Send(request);             
                return Ok(response);
            }
            else return Ok(false);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] DeleteBusinessRequest request)
        {
            DeleteBusinessResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
