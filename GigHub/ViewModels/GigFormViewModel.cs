using GigHub.Dtos;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public GigDto GigDto { get; set; } = new GigDto();
        public IEnumerable<GenreDto> GenreDtos { get; set; } = new List<GenreDto>();
        public string Title => GigDto.Id != 0 ? "Update a Gig" : "Add a Gig";
    }
}