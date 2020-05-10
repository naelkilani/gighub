using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigsRepository GigsRepository { get; }
        IUserRepository UserRepository { get; }
        IGenresRepository GenresRepository { get; }
        void Save();
    }
}