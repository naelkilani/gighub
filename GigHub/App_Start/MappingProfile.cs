using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gig, GigDto>();
            CreateMap<GigDto, Gig>()
                .ForMember(g => g.Active, opt => opt.Ignore());

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            CreateMap<ApplicationUser, ArtistDto>();
        }
    }
}