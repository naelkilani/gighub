using GigHub.Models;
using GigHub.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class GigDto
    {
        private DateTime _dateTime;

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
                if (_dateTime != default)
                    return _dateTime;

                DateTime.TryParse($"{Date} {Time}", out _dateTime);

                return _dateTime;
            }

            set
            {
                _dateTime = value;
            }
        }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public string Month => _dateTime != default
            ? DateTime.ToString("MMM")
            : string.Empty;

        public string Day => _dateTime != default
            ? DateTime.ToString("d ")
            : string.Empty;
    }
}