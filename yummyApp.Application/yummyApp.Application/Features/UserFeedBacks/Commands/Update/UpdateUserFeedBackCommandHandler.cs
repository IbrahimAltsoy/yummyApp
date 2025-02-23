using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Update
{
    public class UpdateUserFeedBackCommandHandler : IRequestHandler<UpdateUserFeedBackCommandRequest, UpdateUserFeedBackCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public UpdateUserFeedBackCommandHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UpdateUserFeedBackCommandResponse> Handle(UpdateUserFeedBackCommandRequest request, CancellationToken cancellationToken)
        {
           
            var existingFeedback = await _repository.GetAsync(x=>x.Id==request.Id);
            if (existingFeedback == null) return new UpdateUserFeedBackCommandResponse
            {
                Message = "Feedback bulunamadı.",
                Success = false
            };
            _mapper.Map(request, existingFeedback);
            var updatedFeedback = await _repository.UpdateAsync(existingFeedback);
            if (updatedFeedback != null)
                return new UpdateUserFeedBackCommandResponse
                {
                    Message = "Güncelleme başarılı bir şekilde oldu.",
                    Success = true
                };
            else
                return new UpdateUserFeedBackCommandResponse
                {
                    Message = "Güncelleme sırasında hata oluştu.",
                    Success = false
                };
        }
    }
}
