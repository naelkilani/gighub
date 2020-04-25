using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        public ApplicationUser User { get; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public Notification Notification { get; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public bool IsRead { get; set; }

        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            User = user ?? throw new ArgumentNullException("user");
            Notification = notification ?? throw new ArgumentNullException("notification");
        }
    }
}