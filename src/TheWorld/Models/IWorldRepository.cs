using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
  public interface IWorldRepository
  {
    IEnumerable<Trip> GetAllTrips();

    Trip GetTripByName(string tripName);

    Task<bool> SaveChangesAsync();


    //SW expresses opinion he prefers to use Entity objects, not view model objects when 
    //dealing with repository.  
    void AddTrip(Trip trip);

    void AddStop(string tripName, Stop newStop);
  }
}