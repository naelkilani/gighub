﻿using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingGigsDtos = _context.Gigs
                .Include(g => g.Artist)
                .Where(g => g.DateTime > DateTime.Now)
                .OrderBy(g => g.DateTime)
                .ToList()
                .Select(Mapper.Map<Gig, GigDto>);

            return View(upcomingGigsDtos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}