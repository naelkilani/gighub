using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
            _userRepository = new UserRepository(_context);
        }

        [HttpPost]
        public IHttpActionResult Post(FollowingDto followingDto)
        {
            var artist = _userRepository.GetUser(followingDto.ArtistId);

            if (artist == null)
                return NotFound();

            var user = _userRepository.GetUserIncludeFollowees(User.Identity.GetUserId());

            //It is better to have separate API for DELETE action.  
            user.ChangeFollowing(followingDto.ArtistId);

            _context.SaveChanges();

            return Ok();
        }
    }
}
