using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Medias.Commands.Delete
{
    public class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommandRequest, DeleteMediaCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IMediaRepository _mediaRepository;

        public DeleteMediaCommandHandler(IMapper mapper, IMediaRepository mediaRepository)
        {
            _mapper = mapper;
            _mediaRepository = mediaRepository;
        }

        public async Task<DeleteMediaCommandResponse> Handle(DeleteMediaCommandRequest request, CancellationToken cancellationToken)
        {
            //var data = _mapper.Map<Media>(request);
            await _mediaRepository.RemovePhotoFromPost(request.Id, request.ImageUrl);
            return new();
        }
    }
}
//string urlToRemove iyi güzel de bunu client tarafında almak zor olmaz mı bunu 1.Foto ya 1 , 2.fotoya 2, 3.Fotoya 3 olarak almak daha kullanışlı olmaz mı sence ama nasıl tasarlayacağım hakkında hiç bir fikrim yok 
//ChatGPT
//Evet, URL'yi client tarafından kaldırmak için dizin numarası gibi bir yaklaşım daha kullanışlı olabilir. Böylece, kullanıcı hangi fotoğrafın kaldırılmasını istediğini belirterek işlemi daha kolay yapabilir. İşte bu yöntemi kullanarak bir örnek:

//İlk olarak, kullanıcıya hangi fotoğrafın kaldırılmasını istediğini seçmesi için bir arayüz sunun. Örneğin, bir sayfa veya bir formda fotoğrafların bir listesi veya galerisi olsun.
//Kullanıcı bir fotoğrafı kaldırmak istediğinde, ilgili fotoğrafın yanında bir "Kaldır" düğmesi veya benzeri bir işlem yapılabilir.
//Kullanıcı "Kaldır" düğmesine tıkladığında, bu işlem için bir istek gönderin ve seçilen fotoğrafın dizin numarasını veya benzersiz bir kimliğini (ID) de gönderin.
//Sunucu tarafında, gelen isteği işleyin ve belirtilen fotoğrafı kaldırın. İsteğin içinde gelen dizin numarası veya kimlikle ilgili fotoğrafı belirleyebilirsiniz.
//Son olarak, güncellenmiş medya listesini client'a geri gönderin ve kullanıcı arayüzünü bu değişikliği yansıtacak şekilde güncelleyin.
//Bu şekilde, kullanıcı hangi fotoğrafı kaldırmak istediğini daha kolay belirleyebilir ve işlem daha kullanıcı dostu olur. Client tarafında, her fotoğrafın bir dizin numarası veya benzersiz bir kimliğe sahip olduğundan emin olun ve bu kimlikleri kullanarak server'a gönderin. Server tarafında da bu kimlikleri kullanarak hangi fotoğrafın kaldırılacağını belirleyin. Bu, URL'lerin değişmesi veya dizin numaralarının güncellenmesi gibi durumları önler ve işlemi daha güvenilir hale getirir.