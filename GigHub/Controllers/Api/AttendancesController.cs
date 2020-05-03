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

            var user = GetUser();

            if (user.IsAttending(gig.Id))
                return BadRequest("The attendance already exists.");

            user.Attending(gig);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var gig = _context.Gigs.FirstOrDefault(g => g.Id == id);

            if (gig == null)
                return NotFound();

            var user = GetUser();

            if (!user.IsAttending(gig.Id))
                return NotFound();

            user.NotAttending(gig);
            _context.SaveChanges();

            return Ok(id);
        }

        private ApplicationUser GetUser()
        {
            var userId = User.Identity.GetUserId();

            return _context.Users
                .Include(u => u.Gigs)
                .First(u => u.Id == userId);
        }

    }
}
