using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigsRepository GigsRepository { get; }
        public IUserRepository UserRepository { get; }
        public IGenresRepository GenresRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            GigsRepository = new GigsRepository(_context);
            UserRepository = new UserRepository(_context);
            GenresRepository = new GenresRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}