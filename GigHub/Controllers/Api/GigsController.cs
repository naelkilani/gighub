using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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
            var gig = _context.Gigs.Include(g => g.Attendees).FirstOrDefault(g => g.Id == id && g.ArtistId == userId);

            if (gig == null || !gig.Active)
                return NotFound();

            gig.Active = false;

            AddCanceledNotifications(gig);

            _context.SaveChanges();

            return Ok();
        }

        private void AddCanceledNotifications(Gig gig)
        {
            var notification = new Notification
            {
                GigId = gig.Id,
                Type = NotificationType.GigCanceled
            };

            _context.Notifications.Add(notification);

            foreach (var attendee in gig.Attendees)
            {
                attendee.Notify(notification);
            }
        }
    }
}
