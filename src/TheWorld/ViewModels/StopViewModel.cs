using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
  /// <summary>
  /// To do a test drive:
  ///   1.  Make sure you've gone to Project/Properties, Turn off Launch URL.
  ///   2.  Use Postman.  http://localhost:2108/api/trips/US%20Trip//stops
  ///   3.  Put this in the body:
  ///   4.  press F5 to run (or ctrl f5)
  ///   5.  Send.. in postman
  /// { 
  //  "name": "Cleveland, OH",
  //  "arrival": "2005-05-15",
  //  "order": 11
  //}
  /// </summary>
  public class StopViewModel
  {
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Name { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [Required]
    public int Order { get; set; }

    [Required]
    public System.DateTime Arrival { get; set; }
  }
}
