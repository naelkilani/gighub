﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        public ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public Notification Notification { get; set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public bool IsRead { get; set; }

        public void Read()
        {
            IsRead = true;
        }
    }
}