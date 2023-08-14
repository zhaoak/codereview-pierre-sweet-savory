using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TreatTracker.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TreatTracker.Controllers;

public class TreatsController : Controller
{
    private readonly TreatTrackerContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, TreatTrackerContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Treat> TreatList = _db.Treats
                            .Include(treat => treat.JoinEntities)
                            .OrderBy(treat => treat.Name)
                            .ToList();
      return View(TreatList);
    }

    public ActionResult Details(int treatId)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == treatId);
      return View(thisTreat);
    }
}
