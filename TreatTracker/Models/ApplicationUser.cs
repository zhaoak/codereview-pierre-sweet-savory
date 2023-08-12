using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TreatTracker.Models;

public class ApplicationUser : IdentityUser
{
  [Required]
  public int EmployeeId; // for pierre's employee id system
}
