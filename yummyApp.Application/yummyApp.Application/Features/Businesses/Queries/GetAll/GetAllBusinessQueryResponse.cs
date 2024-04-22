using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Businesses.Queries.GetAll
{
    public class GetAllBusinessQueryResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string[] Menu { get; set; }
        public string City { get; set; }
        public BusinessQuality Quality { get; set; }
    }
}
