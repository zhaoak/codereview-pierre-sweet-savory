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
      private readonly UserManager<ApplicationUser> _userManager;

      public HomeController(UserManager<ApplicationUser> userManager, TreatTrackerContext db)
      {
        _userManager = userManager;
        _db = db;
      }

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
