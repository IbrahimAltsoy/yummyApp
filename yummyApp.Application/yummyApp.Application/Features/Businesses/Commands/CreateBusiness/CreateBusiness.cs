using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Enums;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Businesses.Commands.CreateBusiness
{
    public class CreateBusinessCommand:IRequest<string>
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
        public Guid OwnerUserId { get; set; }
        public BusinessQuality BusinessQuality { get; set; }
    }
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, string>
    {
        readonly IBusinessRepository _businessRepository;
        readonly IMapper _mapper;
        readonly IPublisher _publisher;

        public CreateBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper, IPublisher publisher)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<string> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Business>(request);
            var result = await  _businessRepository.AddAsync(entity);
             entity.AddDomainEvent(new BusinessCreatedEvent(entity));
            return result.Name;
        }
    }
}
