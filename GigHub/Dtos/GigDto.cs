using GigHub.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Time { get; set; }

        public DateTime DateTime => DateTime.Parse($"{Date} {Time}");

        [Required(AllowEmptyStrings = false)]
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