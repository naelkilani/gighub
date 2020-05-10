using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigsRepository
    {
        Gig GetGig(int id);
        Gig GetGigIncludeAttendees(int id);
        IEnumerable<Gig> GetUpcomingGigs();
        IEnumerable<Gig> GetArtistGigs(string artistId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        void Add(Gig gig);
    }
}