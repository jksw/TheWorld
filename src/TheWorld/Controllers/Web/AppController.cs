using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Web
{
  public class AppController : Controller
  {
    private IMailService _mailService;
    private IConfigurationRoot _config;
    private IWorldRepository _repository;
    private ILogger<AppController> _logger;

    public AppController(IMailService mailService,
      IConfigurationRoot config,
      IWorldRepository repository,
      ILogger<AppController> logger)
    {
      _mailService = mailService;
      _config = config;
      _repository = repository;
      _logger = logger;

    }


    public IActionResult Index()
    {
      try
      {
        return View();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
        return Redirect("/error");
      }
    }

    public IActionResult Trips()
    {
      var trips = _repository.GetAllTrips();
      return View(trips);
    }

    public IActionResult Contact()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Contact(ContactViewModel model)
    {

      if (model.Email.Contains("aol.com"))
      {
        //property error shows where Email is
        ModelState.AddModelError("Email", "We don't support AOL addresses");

        //Model error would show up in validation summary
        //ModelState.AddModelError("", "We don't support AOL addresses");
      }

      if (ModelState.IsValid)


      {
        _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From TheWorld", model.Message);
        ModelState.Clear();

        ViewBag.UserMessage = "Message Sent";
      }

      return View();
    }

    public IActionResult About()
    {
      return View();
    }
  }
}
