
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
  public class AuthController : Controller
  {
    private SignInManager<WorldUser> _signInManager;

    //type ctor and hit tab twice
    //need to inject a signin manager, thus need for constructor

    public AuthController(SignInManager<WorldUser> signInManager)
    {
      _signInManager = signInManager;
    }

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
    /// <summary>
    /// ReturnUrl is a querystring parm
    /// </summary>
    /// <param name="vm"></param>
    /// <param name="ReturnUrl"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel vm, string ReturnUrl)
    {
      if (ModelState.IsValid)
      {
        var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                                              vm.Password,
                                                              true, false);
        //See 3:50 into module "Implement Login and Logout"
        if (signInResult.Succeeded)
        {
          if (string.IsNullOrWhiteSpace(ReturnUrl))
          {
            return RedirectToAction("Trips", "App");
          }
          else
          {
            return Redirect(ReturnUrl);
          }
        }
        else
        {
          //the "" means error on the object, not on a specific field
          ModelState.AddModelError("", "username or password incorrect");
        }
      }
      return View();

    }


    public async Task<ActionResult> Logout()
    {
      if (User.Identity.IsAuthenticated)
      {
        await _signInManager.SignOutAsync();
      }
      return RedirectToAction("Index", "App");
    }
  }
}
