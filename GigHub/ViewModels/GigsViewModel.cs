using GigHub.Dtos;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<GigDto> GigDtos = new List<GigDto>();
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public bool AllowSearch { get; set; }
    }
}