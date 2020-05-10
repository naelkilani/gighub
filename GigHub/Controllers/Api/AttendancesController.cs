using GigHub.Dtos;
using GigHub.Models;
using GigHub.Repositories;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
            _userRepository = new UserRepository(_context);
        }

        [HttpPost]
        public IHttpActionResult Post(AttendanceDto attendanceDto)
        {
            var gig = _context.Gigs.FirstOrDefault(g => g.Id == attendanceDto.GigId);

            if (gig == null)
                return NotFound();

            var user = _userRepository.GetUserIncludeGigs(User.Identity.GetUserId());

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

            var user = _userRepository.GetUserIncludeGigs(User.Identity.GetUserId());

            if (!user.IsAttending(gig.Id))
                return NotFound();

            user.NotAttending(gig);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
