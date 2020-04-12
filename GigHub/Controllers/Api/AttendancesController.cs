﻿using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
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

            if (_context.Attendances.Any(a => a.GigId == attendanceDto.GigId && a.AttendeeId == userId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                GigId = attendanceDto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}