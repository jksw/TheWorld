
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Controllers
{
  public class AuthController : Controller
  {
    public IActionResult Login()
    {
      //User is already authenticated but they somehow ended up on login page. Just send them to trips.
      if (User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Trips", "App");
      }

      //Not authenticated -- take them to a view so they can enter credentials
      return View();
    }
  }
}
