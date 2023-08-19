using Microsoft.AspNetCore.Mvc;
using TreatTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TreatTracker.Controllers
{
    public class HomeController : Controller
    {
      private readonly TreatTrackerContext _db;

      public HomeController(TreatTrackerContext db)
      {
        _db = db;
      }

      [HttpGet("/")]
      public ActionResult Index()
      {
        List<Treat> treatList = _db.Treats.ToList();
        List<Flavor> flavorList = _db.Flavors.ToList();
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("treats", treatList);
        model.Add("flavors", flavorList);
        return View(model);
      }
    }
}
