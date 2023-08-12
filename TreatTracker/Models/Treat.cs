using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreatTracker.Models
{
  public class Treat 
  {
    public int TreatId { get; set; }
    [Required(ErrorMessage = "Treats must have a name.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Treats must have a description.")]
    public string Description { get; set; }
    public string ImageURL { get; set; }
    public ApplicationUser CreatedByUser { get; set; } // for tracking who added what treat

    public List<FlavorTreat> JoinEntities { get;}
  }
}
