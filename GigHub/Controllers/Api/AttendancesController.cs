using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Post(AttendanceDto attendanceDto)
        {
            var gig = _context.Gigs.FirstOrDefault(g => g.Id == attendanceDto.GigId);

            if (gig == null)
                return NotFound();

            var userId = User.Identity.GetUserId();
            var user = _context.Users.Include(u => u.Gigs).First(x => x.Id == userId);

            if (user.Gigs.Any(g => g.Id == gig.Id))
                return BadRequest("The attendance already exists.");

            user.Gigs.Add(gig);
            _context.SaveChanges();

            return Ok();
        }
    }
}
