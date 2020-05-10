using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigsRepository : IGigsRepository
    {
        private readonly ApplicationDbContext _context;

        public GigsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .FirstOrDefault(g => g.Id == id);
        }

        public Gig GetGigIncludeAttendees(int id)
        {
            return _context.Gigs
                .Include(g => g.Attendees)
                .FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetUpcomingGigs()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.Active)
                .ToList();
        }

        public IEnumerable<Gig> GetArtistGigs(string artistId)
        {
            return _context.Gigs
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && g.Active)
                .ToList();
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Users
                .Include(u => u.Gigs.Select(g => g.Genre))
                .Include(u => u.Gigs.Select(g => g.Artist))
                .First(u => u.Id == userId)
                .Gigs
                .Where(g => g.DateTime > DateTime.Now)
                .ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}