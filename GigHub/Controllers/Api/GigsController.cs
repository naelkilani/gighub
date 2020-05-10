using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Models;
using GigHub.Persistence;

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
            var gig = _context.Gigs.Include(g => g.Attendees).FirstOrDefault(g => g.Id == id && g.ArtistId == userId);

            if (gig == null || !gig.Active)
                return NotFound();

            gig.Cancel();

            _context.SaveChanges();

            return Ok();
        }
    }
}
