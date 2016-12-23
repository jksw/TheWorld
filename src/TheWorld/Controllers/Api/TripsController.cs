
//got 404 when accidentally did 'using ... aspet.mvc.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
  //[Route("api/[controller]")]
  public class TripsController : Controller
  {

    //If you want to test with Postman tool, go to Project Proeprties  Debug -- uncheck Launch URL
    //Use Postman in conj. with the Httpget attribute below

    [HttpGet("api/trips")]
    public IActionResult Get()
    { 
      return Ok(new Trip() { Name = "My Trip" });
    }
  }
}
