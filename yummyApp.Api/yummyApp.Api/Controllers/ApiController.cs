using MediatR;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        readonly IGooglePlacesService _googlePlacesService;
        readonly IMediator _mediator;
        
        readonly IConfiguration _configuration;

        public ApiController(IGooglePlacesService googlePlacesService, IMediator mediator, IConfiguration configuration)
        {
            _googlePlacesService = googlePlacesService;
            _mediator = mediator;
           
            _configuration = configuration;
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
        [HttpGet("test-log")]
        public IActionResult TestLog()
        {
            throw new Exception("Bu test hatasıdır! Serilog logluyor mu kontrol et.");
        }
        [HttpGet("test-db-log")]
        public IActionResult TestDbLog()
        {
            throw new Exception("");
        }
        [HttpPost("test-email-error")]
        public IActionResult TestEmailError()
        {
            try
            {
                throw new ForbiddenAccessException();
            }
            catch (Exception ex)
            {
                
                throw; // Hata tekrar fırlatılır
            }
        }


    }
}
