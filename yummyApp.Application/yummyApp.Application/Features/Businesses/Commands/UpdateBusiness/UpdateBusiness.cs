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
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string[] Menu { get; set; }
        public string City { get; set; }
        public BusinessQuality Quality { get; set; }
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
