using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Delete
{
    public class DeleteUserFeedBackCommandHandler : IRequestHandler<DeleteUserFeedBackCommandRequest, DeleteUserFeedBackCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public DeleteUserFeedBackCommandHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DeleteUserFeedBackCommandResponse> Handle(DeleteUserFeedBackCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<UserFeedback>(request);
           var deletedData =  await _repository.DeleteAsync(data, permanent:true);
            if (deletedData !=null) return new DeleteUserFeedBackCommandResponse() { Message = "Feedback başarıyla silindi.", Success = true };
            return new DeleteUserFeedBackCommandResponse() { Message ="Feedback bilgisi silinemedi.",Success = false };


        }
    }
}
