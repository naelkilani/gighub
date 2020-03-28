﻿using GigHub.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(244)]
        public string Venue { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }
    }
}