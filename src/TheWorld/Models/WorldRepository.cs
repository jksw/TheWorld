using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
  public class WorldRepository : IWorldRepository
  {
    private WorldContext _context;
    private ILogger<WorldRepository> _logger;

    public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
    {
      _context = context;
      _logger = logger;

    }

    public void AddStop(string tripName, Stop newStop, string username)
    {
      var trip = GetUserTripByName(tripName, username);

      if (trip != null)
      {
        trip.Stops.Add(newStop);       //Foreign key is being set
        _context.Stops.Add(newStop);   //Actually being added
      }
    }

    public void AddTrip(Trip trip)
    {
      _context.Add(trip);

    }

    public IEnumerable<Trip> GetAllTrips()
    {
      _logger.LogInformation("Getting all trips from the database");

      return _context.Trips.ToList();

    }

    public IEnumerable<Trip> GetAllTripsBuUsername(string name)
    {
      return _context.Trips
        .Include(t => t.Stops)
        .Where(t => t.UserName == name)
        .OrderBy(t => t.Name)
        .ToList();
    }

    public Trip GetTripByName(string tripName)
    {
      return _context.Trips
        .Include(t => t.Stops)
        .Where(t => t.Name == tripName)
        .FirstOrDefault();
    }

    public Trip GetUserTripByName(string tripName, string name)
    {
      return _context.Trips
        .Include(t => t.Stops)
        .Where(t => t.Name == tripName && t.UserName == name)
        .FirstOrDefault();
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }


  }
}
