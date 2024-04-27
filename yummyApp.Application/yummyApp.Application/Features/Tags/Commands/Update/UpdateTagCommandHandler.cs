using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.Tags.Commands.Update
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommandRequest, UpdateTagCommandResponse>
    {
        readonly IMapper _mapper;
        readonly ITagRepository _tagRepository;

        public UpdateTagCommandHandler(IMapper mapper, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<UpdateTagCommandResponse> Handle(UpdateTagCommandRequest request, CancellationToken cancellationToken)
        {
           var data = await _tagRepository.GetAsync(x=>x.Id == request.Id);
            data.BusinessID = request.BusinessID;
            data.PostID = request.PostID;
            await _tagRepository.UpdateAsync(data);
            return new();
        }
    }
}
