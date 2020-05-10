using GigHub.Models;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUserIncludeGigs(string id)
        {
            return _context.Users
                .Include(u => u.Gigs)
                .FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUserIncludeFollowees(string id)
        {
            return _context.Users
                .Include(u => u.Followees)
                .FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUserIncludeGigsAndFollowees(string id)
        {
            return _context.Users
                .Include(u => u.Followees)
                .Include(u => u.Gigs)
                .FirstOrDefault(u => u.Id == id);
        }
    }
}