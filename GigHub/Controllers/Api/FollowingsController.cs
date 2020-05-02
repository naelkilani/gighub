using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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

            var user = GetUser();

            user.ChangeFollowing(followingDto.ArtistId);

            _context.SaveChanges();

            return Ok();
        }

        private ApplicationUser GetUser()
        {
            var userId = User.Identity.GetUserId();

            return _context.Users.Include(u => u.Followees).First(u => u.Id == userId);
        }
    }
}
