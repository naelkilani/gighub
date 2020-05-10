using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUser(string id);
        ApplicationUser GetUserIncludeGigs(string id);
        ApplicationUser GetUserIncludeFollowees(string id);
        ApplicationUser GetUserIncludeGigsAndFollowees(string id);
    }
}