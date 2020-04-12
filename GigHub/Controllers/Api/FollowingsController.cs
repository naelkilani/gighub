using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Post(FollowingDto followingDto)
        {
            var artist = _context.Users.FirstOrDefault(u => u.Id == followingDto.ArtistId);

            if (artist == null)
                return NotFound();

            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId &&
                                             f.FolloweeId == followingDto.ArtistId))
                return BadRequest("The following already exist.");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.ArtistId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
