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
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .OrderBy(g => g.DateTime)
                .Select(Mapper.Map<Gig, GigDto>);

            return View(myGigDtos);
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            // This is bad code adding Attendance table instead of Link Table is better.
            var attendingGigsIds = _context.Users
                .Include(u => u.Gigs)
                .First(x => x.Id == userId)
                .Gigs
                .Select(g => g.Id)
                .ToList();

            var attendingGigsDtos = _context.Gigs
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .Where(g => attendingGigsIds.Contains(g.Id) && g.DateTime > DateTime.Now)
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
            var gig = _context.Gigs.FirstOrDefault(g => g.Id == id);

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
            var gig = _context.Gigs.First(g => g.Id == gigDto.Id);
            Mapper.Map(gigDto, gig);
        }
    }
}