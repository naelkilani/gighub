using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

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