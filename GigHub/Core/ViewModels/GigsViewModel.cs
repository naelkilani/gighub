using System.Collections.Generic;
using GigHub.Core.Dtos;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<GigDto> GigDtos = new List<GigDto>();
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public bool AllowSearch { get; set; }
    }
}