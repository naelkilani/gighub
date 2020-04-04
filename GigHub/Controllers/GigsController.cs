using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
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

            return RedirectToAction("Index", "Home");
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