using MediatR;
using yummyApp.Application.Dtos.GoogleApis.PlaceDetail;

namespace yummyApp.Application.Features.Apis.Queries.GetNearByPlaceDetail
{
    public class GetNearByPlaceDetailQueryRequest:IRequest<PlaceDetailResult>
    {
        public string? PlaceId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
       
    }
}
