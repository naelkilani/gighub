using GigHub.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IGigsRepository GigsRepository { get; }
        IUserRepository UserRepository { get; }
        IGenresRepository GenresRepository { get; }
        void Save();
    }
}