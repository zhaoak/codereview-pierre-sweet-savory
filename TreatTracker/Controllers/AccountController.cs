using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TreatTracker.Models;
using System.Threading.Tasks;
using TreatTracker.ViewModels;

namespace TreatTracker.Controllers;

public class AccountController : Controller {
  private readonly TreatTrackerContext _db;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;

  public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TreatTrackerContext db)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _db = db;
  }

  // login page
  public ActionResult Index()
  {
    return View();
  }

  // registration page
  public IActionResult Register()
  {
    return View();
  }
  
  // registration post route
  [HttpPost]
  public async Task<ActionResult> Register (RegisterViewModel model)
  {
    // model state is valid?
    if (!ModelState.IsValid)
    {
      // return to registration view if not valid, display errors
      return View(model);
    }
    // valid
    else
    {
      // create new user, try to update db
      ApplicationUser user = new ApplicationUser { UserName = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        // if success, redirect to site homepage
        return RedirectToAction("Index", "Home");
      }
      else
      {
        // otherwise return to register page with errors
        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
        return View(model);
      }
    }
  }
}
