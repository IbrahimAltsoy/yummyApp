using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Businesses.Queries.GetById
{
    public class GetByIdQueryResponse
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public string OpeningHours { get; set; }
        public string SocialMediaLinks { get; set; }
        public string Logo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public BusinessQuality BusinessQuality { get; set; }
        public Guid OwnerUserId { get; set; }
    }
}
