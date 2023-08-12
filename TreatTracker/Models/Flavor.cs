using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreatTracker.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    [Required(ErrorMessage = "Flavors must have a name.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Flavors must have a description so customers know what to expect!")]
    public string Description { get; set; }
    public ApplicationUser CreatedByUser { get; set; } // for tracking who added what flavor

    public List<FlavorTreat> JoinEntities { get; }
  }
}
