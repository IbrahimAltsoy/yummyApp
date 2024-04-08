using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.UnitOfWork;
using yummyApp.Persistance.Context;
using yummyApp.Persistance.Repositories;

namespace yummyApp.Persistance.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly YummyAppDbContext _dbContext;

        public UnitOfWork(YummyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IBusinessRepository _businessRepository;
        public IBusinessRepository BusinessRepository =>_businessRepository ??= new BusinessRepository(_dbContext);

        private ICommentRepository _commentRepository;
        public ICommentRepository CommentRepository =>_commentRepository ??= new CommentRepository(_dbContext);

        private IFriendShipRepository _friendShipRepository;
        public IFriendShipRepository FriendShipRepository => _friendShipRepository ??= new FriendShipRepository(_dbContext);

        private ILikeRepository _likeRepository;
        public ILikeRepository LikeRepository =>_likeRepository ??= new LikeRepository(_dbContext);

        private IMediaRepository _mediaRepository;
        public IMediaRepository MediaRepository => _mediaRepository ??= new MediaRepository(_dbContext);

        private IPostRepository _postRepository;
        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_dbContext);

        ITagRepository _tagRepository;
        public ITagRepository TagRepository => _tagRepository ??= new TagRepository(_dbContext);

        private IUserRepository _userRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
           return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
           _dbContext?.Dispose();
        }
    }
}
