using GigHub.Models;
using System;

namespace GigHub.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigDto Gig { get; set; }
        public string OriginalDateAndTime => OriginalDateTime == null ? string.Empty : $"{OriginalDateTime.Value:d MMM yyyy} {OriginalDateTime.Value:HH:mm}";
    }
}