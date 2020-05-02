using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var myGigDtos = _context.Gigs
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && g.Active)
                .OrderBy(g => g.DateTime)
                .Select(Mapper.Map<Gig, GigDto>);

            return View(myGigDtos);
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var attendingGigsDtos = _context.Users
                .Include(u => u.Gigs.Select(g => g.Genre))
                .Include(u => u.Gigs.Select(g => g.Artist))
                .First(u => u.Id == userId)
                .Gigs
                .Where(g => g.DateTime > DateTime.Now)
                .OrderBy(g => g.DateTime)
                .Select(Mapper.Map<Gig, GigDto>);

            var viewModel = new GigsViewModel
            {
                GigDtos = attendingGigsDtos,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new GigFormViewModel
            {
                GenreDtos = _context.Genres.Select(Mapper.Map<Genre, GenreDto>).ToList()
            };

            return View("GigForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.FirstOrDefault(g => g.Id == id && g.ArtistId == userId);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigFormViewModel
            {
                GigDto = Mapper.Map<Gig, GigDto>(gig),
                GenreDtos = _context.Genres.Select(Mapper.Map<Genre, GenreDto>).ToList()
            };

            return View("GigForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(GigDto gigDto)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new GigFormViewModel
                {
                    GigDto = gigDto,
                    GenreDtos = _context.Genres.Select(Mapper.Map<Genre, GenreDto>).ToList()
                };

                return View("GigForm", viewModel);
            }

            if (gigDto.Id == 0)
                AddGig(gigDto);
            else
                UpdateGig(gigDto);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        private void AddGig(GigDto gigDto)
        {
            var gig = Mapper.Map<GigDto, Gig>(gigDto);
            gig.ArtistId = User.Identity.GetUserId();

            _context.Gigs.Add(gig);
        }

        private void UpdateGig(GigDto gigDto)
        {
            var gig = _context.Gigs.Include(g => g.Attendees).First(g => g.Id == gigDto.Id);
            
            gig.Modify(gigDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Search(string searchTerm)
        {
            return RedirectToAction("Index", "Home", new {query = searchTerm});
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .FirstOrDefault(g => g.Id == id);

            if (gig == null)
                return HttpNotFound();

            var model = new GigDetailsViewModel
            {
                Gig = Mapper.Map<Gig, GigDto>(gig)
            };

            if (!User.Identity.IsAuthenticated)
                return View(model);

            var user = GetUser();

            model.IsFollowing = user.IsFollowing(gig.Artist.Id);
            model.IsAttending = user.IsGoing(gig.Id);

            return View(model);
        }

        private ApplicationUser GetUser()
        {
            var userId = User.Identity.GetUserId();

            return _context.Users
                .Include(u => u.Followees)
                .Include(u => u.Gigs)
                .First(u => u.Id == userId);
        }
    }
}