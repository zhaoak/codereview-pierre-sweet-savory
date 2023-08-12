using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TreatTracker.Models;

public class ApplicationUser : IdentityUser
{
  [Required]
  public int Name { get; set; } // username displayed in header, does not have to be unique
}
