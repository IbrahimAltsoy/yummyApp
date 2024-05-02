using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Businesses.Commands.UpdateBusiness
{
    public class UpdateBusinessCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
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
    public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, bool>
    {
        
        readonly IMapper _mapper;
        readonly IBusinessRepository _businessRepository;

        public UpdateBusinessCommandHandler( IMapper mapper, IBusinessRepository businessRepository)
        {
           _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Business>(request);
            await _businessRepository.UpdateAsync(entity);
            return true;
        }
    }
}
