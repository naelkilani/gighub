using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                GenreDtos = _context.Genres.Select(Mapper.Map<Genre, GenreDto>).ToList()
            };

            return View(viewModel);
        }
    }
}