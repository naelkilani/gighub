using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.FirstOrDefault(g => g.Id == id && g.ArtistId == userId);

            if (gig == null || !gig.Active)
                return NotFound();

            gig.Active = false;
            _context.SaveChanges();

            return Ok();
        }
    }
}
