using MediatR;
using yummyApp.Application.Dtos.GoogleApis;

namespace yummyApp.Application.Features.Apis.Queries.GetNearByPlaces
{
    public class GetNearbyPlacesQueryRequest : IRequest<PlaceSearchResult>
    {
        public string Category { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Radius { get; set; }
    }
}
//string category,double latitude, double longitude, int radius