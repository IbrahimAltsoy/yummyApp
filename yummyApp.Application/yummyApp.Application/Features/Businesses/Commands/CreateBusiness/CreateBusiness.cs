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
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string[] Menu { get; set; }
        public string City { get; set; }
        public BusinessQuality Quality { get; set; }
        //public List<Tag>? Tags { get; set; }
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
