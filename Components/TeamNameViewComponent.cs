using Microsoft.AspNetCore.Mvc;
using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAssignment.Components
{
    //collects the data for the partial view
    public class TeamNameViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;

        public TeamNameViewComponent (BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {


            return View(context.Teams
                .Distinct()
                .OrderBy(x=>x)
                .ToList());
        }
    }
}