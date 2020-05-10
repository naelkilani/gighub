using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigsRepository _gigsRepository;
        private readonly UserRepository _userRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _gigsRepository = new GigsRepository(_context);
            _userRepository = new UserRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigsQuery = _gigsRepository.GetUpcomingGigs();

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

            var user = _userRepository.GetUserIncludeGigs(User.Identity.GetUserId());
            viewModel.GigDtos.ForEach(g => g.IsGoing = user.IsAttending(g.Id));

            return View("Gigs", viewModel);
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