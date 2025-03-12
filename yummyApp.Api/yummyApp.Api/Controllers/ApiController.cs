using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using yummyApp.Application.Exceptions;
using yummyApp.Application.Features.Apis.Queries.GetNearByPlaceDetail;
using yummyApp.Application.Features.Apis.Queries.GetNearByPlaces;
using yummyApp.Application.Services.GoogleApi;
using yummyApp.Persistance.Services.Logging;

namespace yummyApp.Api.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
       
        readonly IMediator _mediator;

        public ApiController(IMediator mediator)
        {           
            _mediator = mediator;          
        }

        [HttpGet("get-nearby-places")]
        public async Task<IActionResult> GetNearbyPlaces([FromQuery]GetNearbyPlacesQueryRequest request)
        {
           var response = await _mediator.Send(request);

            return Ok(response.Results);
        }
        [HttpGet("get-business-detail/{id}")]
        public async Task<IActionResult> GetPlaceDetailAndReviews([FromRoute] string id, [FromQuery] double? latitude, [FromQuery] double? longitude)
        {
            var request = new GetNearByPlaceDetailQueryRequest { PlaceId = id, Latitude = latitude, Longitude = longitude };
            var data = await _mediator.Send(request);

            return Ok(data);
        }
    }
}
