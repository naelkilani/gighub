using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            var userId = User.Identity.GetUserId();

            var notificationDtos = _context.Users
                .Include(u => u.UserNotifications.Select(un => un.Notification.Gig.Artist))
                .First(u => u.Id == userId)
                .UserNotifications
                .Where(un => !un.IsRead)
                .Select(un => un.Notification)
                .Select(Mapper.Map<Notification, NotificationDto>);

            return Ok(notificationDtos);
        }
    }
}
