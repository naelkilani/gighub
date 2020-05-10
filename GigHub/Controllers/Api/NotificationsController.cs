using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;

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

        [HttpGet]
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

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var notReadNotifications = _context.Users
                .Include(u => u.UserNotifications)
                .First(u => u.Id == userId)
                .UserNotifications
                .Where(un => !un.IsRead)
                .ToList();

            notReadNotifications.ForEach(un => un.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
