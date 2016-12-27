using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{

  [Route("/api/trips/{tripName}/stops")]
  public class StopsController : Controller
  {
    private GeoCoordsService _coordsService;
    private ILogger<TripsController> _logger;
    private IWorldRepository _repository;

    public StopsController(IWorldRepository repository, 
        ILogger<TripsController> logger,
        GeoCoordsService coordsService)
      

    {
      _repository = repository;
      _logger = logger;
      _coordsService = coordsService;

    }

    [HttpGet("")]
    public IActionResult Get(string tripName)
    {
      try
      {
        var trip = _repository.GetTripByName(tripName);

        //TODO I don't understand this syntax.  This is madness.
        return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get stops: {ex}");
      }
      return BadRequest("Failed to get stops");
    }

    [HttpPost("")]
    public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
    {
      try
      {
        // if the VM is valid
        if (ModelState.IsValid)
        {
          var newStop = Mapper.Map<Stop>(vm);

          //Lookup the Geocodes
          var result = await _coordsService.GetCoordsAsync(newStop.Name);

          if (!result.Success)
          {
            _logger.LogError(result.Message);
          }

          else
          {
            newStop.Latitude = result.Latitude;
            newStop.Longitude = result.Longitude;
          }


          _repository.AddStop(tripName, newStop);

          if (await _repository.SaveChangesAsync())
          {
            // Save to the database
            return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
          }
          
        }
      }
      catch (Exception ex)
      {
        _logger.LogError("Failed to save new Stop: {0}", ex);
      }

      return BadRequest("Failed to save new stop");
    }
  }
}


