using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;


namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artistDtos = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList()
                .Select(Mapper.Map<ApplicationUser, ArtistDto>);

            return View(artistDtos);
        }
    }
}