using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TreatTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TreatTracker.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly TreatTrackerContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, TreatTrackerContext db)
    {
      _db = db;
      _userManager = userManager;
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

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor)
    {
      if (!ModelState.IsValid)
      {
        // if not valid, redirect to create page 
        return View(flavor);
      }
      else
      {
        // if valid
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        flavor.CreatedByUser = currentUser;
        _db.Flavors.Add(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      if (!ModelState.IsValid)
      {
        return View(flavor);
      }
      else
      {
        _db.Flavors.Update(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    
    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddTreat(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(thisFlavor);
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int treatId)
    {
      // check if treat-flavor relationship already exists
      #nullable enable
      FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == treatId && join.FlavorId == flavor.FlavorId));
      #nullable disable

      // if relationship doesn't exist AND at least one treat exists
      if (joinEntity == null && treatId != 0)
      {
        // then create and add the new relationship
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavor.FlavorId, TreatId = treatId });
        _db.SaveChanges();
      }
      // then redirect to the details page
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
