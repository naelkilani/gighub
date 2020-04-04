using GigHub.Models;
using GigHub.Validation;
using Microsoft.Ajax.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [Time]
        public string Time { get; set; }

        public DateTime DateTime
        {
            get
            {
                if (Date.IsNullOrWhiteSpace() ||
                    Time.IsNullOrWhiteSpace() ||
                    !DateTime.TryParse($"{Date} {Time}", out var dateTime))
                    return new DateTime();

                return dateTime;
            }
        }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
    }
}