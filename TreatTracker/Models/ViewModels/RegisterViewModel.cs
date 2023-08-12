using System.ComponentModel.DataAnnotations;

namespace TreatTracker.ViewModels;

public class RegisterViewModel
{
  [Required]
  [EmailAddress]
  [Display(Name = "Email Address: ")]
  public string Email { get; set; }

  [Required]
  [Display(Name = "Display Name: ")]
  public string DisplayName { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$",
      ErrorMessage = "Password must contain at least six characters, a lowercase and uppercase letter, a number, and a special character in order to safeguard the treats.")]
  public string Password { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Confirm password: ")]
  [Compare("Password", ErrorMessage = "Password and password confirmation do not match!")]
  public string ConfirmPassword { get; set; }
}
