using AutoMapper;
using GigHub.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required] 
        public bool Active { get; private set; } = true;

        public ICollection<ApplicationUser> Attendees { get; set; }

        public Gig()
        {
            Attendees = new Collection<ApplicationUser>();
        }

        public void Cancel()
        {
            Active = false;

            var notification = Notification.GigCanceled(this);

            Notify(notification);
        }

        public void Modify(GigDto gigDto)
        {
            var originalDateTime = DateTime;
            var originalVenue = Venue;

            Mapper.Map(gigDto, this);

            if (originalDateTime == DateTime && originalVenue == Venue) 
                return;

            var notification = Notification.GigUpdated(this, originalDateTime, originalVenue);

            Notify(notification);
        }

        public void Notify(Notification notification)
        {
            foreach (var attendee in Attendees)
            {
                attendee.Notify(notification);
            }
        }
    }
}