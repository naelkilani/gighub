using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.Persistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public ActionResult Mine()
        {
            var myGigDtos = _unitOfWork.GigsRepository.GetArtistGigs(User.Identity.GetUserId())
                .OrderBy(g => g.DateTime)
                .Select(Mapper.Map<Gig, GigDto>);

            return View(myGigDtos);
        }

        public ActionResult Attending()
        {
            var attendingGigsDtos = _unitOfWork.GigsRepository.GetGigsUserAttending(User.Identity.GetUserId())
                .OrderBy(g => g.DateTime)
                .Select(Mapper.Map<Gig, GigDto>)
                .ToList();

            attendingGigsDtos.ForEach(g => g.IsGoing = true);

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
                GenreDtos = _unitOfWork.GenresRepository.GetGenres().Select(Mapper.Map<Genre, GenreDto>)
            };

            return View("GigForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.GigsRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                GigDto = Mapper.Map<Gig, GigDto>(gig),
                GenreDtos = _unitOfWork.GenresRepository.GetGenres().Select(Mapper.Map<Genre, GenreDto>)
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
                    GenreDtos = _unitOfWork.GenresRepository.GetGenres().Select(Mapper.Map<Genre, GenreDto>)
                };

                return View("GigForm", viewModel);
            }

            if (gigDto.Id == 0)
                AddGig(gigDto);
            else
                UpdateGig(gigDto);

            _unitOfWork.Save();

            return RedirectToAction("Mine", "Gigs");
        }

        private void AddGig(GigDto gigDto)
        {
            var gig = Mapper.Map<GigDto, Gig>(gigDto);
            gig.ArtistId = User.Identity.GetUserId();

            _unitOfWork.GigsRepository.Add(gig);
        }

        private void UpdateGig(GigDto gigDto)
        {
            var gig = _unitOfWork.GigsRepository.GetGigIncludeAttendees(gigDto.Id);
            
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
            var gig = _unitOfWork.GigsRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var model = new GigDetailsViewModel
            {
                Gig = Mapper.Map<Gig, GigDto>(gig)
            };

            if (!User.Identity.IsAuthenticated)
                return View(model);

            var user = _unitOfWork.UserRepository.GetUserIncludeGigsAndFollowees(User.Identity.GetUserId());

            model.IsFollowing = user.IsFollowing(gig.Artist.Id);
            model.IsAttending = user.IsAttending(gig.Id);

            return View(model);
        }
    }
}