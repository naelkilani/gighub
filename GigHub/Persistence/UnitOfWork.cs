using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public GigsRepository GigsRepository { get; }
        public UserRepository UserRepository { get; }
        public GenresRepository GenresRepository { get; }

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