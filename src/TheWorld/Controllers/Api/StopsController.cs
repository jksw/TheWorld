using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{

  [Route("/api/trips/{tripName}/stops")]
  public class StopsController : Controller
  {
    private ILogger<TripsController> _logger;
    private IWorldRepository _repository;

    public StopsController(IWorldRepository repository, ILogger<TripsController> logger)

    {
      _repository = repository;
      _logger = logger;
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
  }
}

