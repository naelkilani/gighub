using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private const int MinimumNotificationToDisplay = 5;

        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var userId = User.Identity.GetUserId();

            var userNotifications = _context.Users
                .Include(u => u.UserNotifications.Select(un => un.Notification.Gig.Artist))
                .First(u => u.Id == userId)
                .UserNotifications;

            var unreadNotifications = GetUnreadNotifications(userNotifications);

            if (unreadNotifications.Count - MinimumNotificationToDisplay > 0)
                return Ok(unreadNotifications);

            var notifications = userNotifications
                .Select(un => un.Notification)
                .OrderByDescending(u => u.DateTime)
                .Take(MinimumNotificationToDisplay)
                .Select(Mapper.Map<Notification, NotificationDto>);

            return Ok(notifications);
        }

        private List<NotificationDto> GetUnreadNotifications(ICollection<UserNotification> userNotifications)
        {
            return userNotifications
                .Where(un => !un.IsRead)
                .Select(un => un.Notification)
                .Select(Mapper.Map<Notification, NotificationDto>)
                .ToList();
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
