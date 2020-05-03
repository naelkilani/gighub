using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigsQuery = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.Active);

            if (!query.IsNullOrWhiteSpace())
                upcomingGigsQuery = upcomingGigsQuery.Where(g =>
                    g.Artist.Name.Contains(query) ||
                    g.Genre.Name.Contains(query) ||
                    g.Venue.Contains(query));
            
            var upcomingGigsDtos = upcomingGigsQuery
                .OrderBy(g => g.DateTime)
                .Select(Mapper.Map<Gig, GigDto>)
                .ToList();

            var viewModel = new GigsViewModel
            {
                GigDtos = upcomingGigsDtos,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                AllowSearch = true
            };

            if (!User.Identity.IsAuthenticated)
                return View("Gigs", viewModel);

            var user = GetUser();
            viewModel.GigDtos.ForEach(g => g.IsGoing = user.IsAttending(g.Id));

            return View("Gigs", viewModel);
        }

        private ApplicationUser GetUser()
        {
            var userId = User.Identity.GetUserId();

            return _context.Users
                .Include(u => u.Gigs)
                .First(u => u.Id == userId);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}