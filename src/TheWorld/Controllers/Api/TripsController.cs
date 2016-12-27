
//got 404 when accidentally did 'using ... aspet.mvc.

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
  [Route("api/trips")]
  public class TripsController : Controller
  {
    private IWorldRepository _repository;

    public TripsController(IWorldRepository repository)
    {
      _repository = repository;

    }

    //Json returned is camelcased, not Pascal cased; diverges from SW's 'returning data' module
    //where we have to convert Pascal case to camel case.
    [HttpGet("")]
    public IActionResult Get()
    {
      try
      {
        var result = _repository.GetAllTrips();
        return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
      }
      catch (Exception ex)
      {
        //TODO logging
        return BadRequest("Error occurred");
      }
    }


    //The post below lead into SW discussing need for AutoMapper -- need to tell method below
    //where, in the post, the data is coming from.  URL?  Parms?  Need to specify.
    //So [FromBody] used...and it results in "Model Bind" taking place. Model Bind means
    //connect the data coming in from the Post, to the object theTrip.
    [HttpPost("")]
    public IActionResult Post([FromBody]TripViewModel theTrip)
    {
      //need to convert view model to real object
      //AutoMapper used here to create reusable mapping between object types, rather than
      //manually doing it 
      if (ModelState.IsValid)
      {
        var newTrip = Mapper.Map<Trip>(theTrip);
        {

        }

        //DateCreated returns a 201 hppt status code.
        return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
      }
      return BadRequest(ModelState);


    }


    #region initialCode
    //If you want to test with Postman tool:
    // 1.  go to Project Properties,  Debug -- uncheck Launch URL
    // 2.  run this site with ctrl F5 to have IISEXPRESS running, otherwise
    // See how there is no browser launched, but if you check the system tray, you'll
    // see that IISEXPRESS is running.  That's what you want.


    // just first try.  Move on from here to using the 200 response code with OK
    ////////[HttpGet("api/trips")]
    ////////public JsonResult Get()
    ////////{
    ////////  return Json(new Trip() { Name = "Steve" });
    ////////}


    //Makes the point that returning http error codes is better way to do this
    //////[HttpGet("api/trips")]
    //////public IActionResult Get()
    //////{
    //////  if (true) return BadRequest("bad things happen");

    //////  return Ok(new Trip() { Name = "My Trip" });
    //////}


    //////[HttpGet("api/trips")]
    //////public IActionResult Get()
    //////{
    //////  if (true) return BadRequest("bad things happen");

    //////  return Ok(new Trip() { Name = "My Trip" });
    //////}
    #endregion


  }

}
