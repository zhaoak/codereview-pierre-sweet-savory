using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TreatTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace TreatTracker.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly TreatTrackerContext _db;

    public FlavorsController(TreatTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      return View(model);
    }

    public ActionResult Details(int id)
    {
      Flavor thisFlavor = _db.Flavors
                          .Include(flavor => flavor.CreatedByUser)
                          .Include(flavor => flavor.JoinEntities)
                          .ThenInclude(join => join.Treat)
                          .FirstOrDefault(flavor => flavor.FlavorId== id);
      return View(thisFlavor);
    }
  }
}
