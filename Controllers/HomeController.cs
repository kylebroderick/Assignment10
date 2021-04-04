﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FinalAssignment.Models;
using FinalAssignment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private BowlingLeagueContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            _context = ctx;
        }

        public IActionResult Index(long? teamId, string teamName, int pageNum = 0)
        {
            int pageSize = 5;

            ViewBag.SelectedTeam = RouteData?.Values["TeamName"];

            return View(new IndexViewModel
            {
                Bowlers = (_context.Bowlers
                    .Where(t => t.TeamId == teamId || teamId == null)
                    .OrderBy(p => p.BowlerFirstName)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,


                    TotalNumItems = (teamId == null ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(x => x.TeamId == teamId).Count())
                },

                TeamCategory = teamName
            });


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}