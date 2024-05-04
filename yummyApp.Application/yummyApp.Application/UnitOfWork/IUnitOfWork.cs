using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IBusinessRepository BusinessRepository { get; }
        ICommentRepository CommentRepository { get; }
        IFriendShipRepository FriendShipRepository { get; }
        ILikeRepository LikeRepository { get; }
        IMediaRepository MediaRepository { get; }
        IPostRepository PostRepository { get; }
        ITagRepository TagRepository { get; }
        //IUserRepository UserRepository { get; }
        int Commit();

        Task<int> CommitAsync();
    }
}
