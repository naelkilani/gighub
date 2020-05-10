using System.Collections.Generic;
using GigHub.Core.Dtos;

namespace GigHub.Core.ViewModels
{
    public class GigFormViewModel
    {
        public GigDto GigDto { get; set; } = new GigDto();
        public IEnumerable<GenreDto> GenreDtos { get; set; } = new List<GenreDto>();
        public string Heading => GigDto.Id != 0 ? "Update a Gig" : "Add a Gig";
    }
}