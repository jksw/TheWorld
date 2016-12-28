using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
  /// <summary>
  /// 
  /// </summary>
  public class GeoCoordsService
  {
    private IConfigurationRoot _config;
    private ILogger<GeoCoordsService> _logger;

    public GeoCoordsService(ILogger<GeoCoordsService> logger, IConfigurationRoot config)
    {
      _logger = logger;
      _config = config;
    }

    //http://www.jerriepelser.com/blog/aspnet-core-no-more-worries-about-checking-in-secrets/
    //Link to how to set enviro variables (properties/computer.. advanced)
    // Stuck:  I have to use ADMWESTON account when setting env variables "Use variables".  So
    //         I will create as system variable.
    //   be sure to exit VS, re-launch anytime you alter env variables using Computer Properies
    //   -- Note, setting env variables in project properties results in them being saved to 
    //      c:\TEMP\TheWorld\src\TheWorld\Properties\launchSettings.json, WHICH IS NOT what 
    //      you want for secrets.
    //      
    //      Note -- there is a way to save secrets at dev time, built into asp.net core, 
    //             but it's the usual convoluted MS approach and I don't understand it and I don't
    //             know how it migrates to production.

    public async Task<GeoCoordsResult> GetCoordsAsync(string name)
    {
      var result = new GeoCoordsResult()
      {
        Success = false,
        Message = "Failed to get coordinates"
      };

      //This is how you should do this -- from config, which is from environment variable
      //but if you set an env variable, exit vs and relaunch it.
      var apiKey = _config["Keys:BingKey"];

     
      var encodedName = WebUtility.UrlEncode(name);
      var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

      var client = new HttpClient();
      var json = await client.GetStringAsync(url);

      // Read out the results
      // Fragile, might need to change if the Bing API changes
      var results = JObject.Parse(json);
      var resources = results["resourceSets"][0]["resources"];
      if (!resources.HasValues)
      {
        result.Message = $"Could not find '{name}' as a location";
      }
      else
      {
        var confidence = (string)resources[0]["confidence"];
        if (confidence != "High")
        {
          result.Message = $"Could not find a confident match for '{name}' as a location";
        }
        else
        {
          var coords = resources[0]["geocodePoints"][0]["coordinates"];
          result.Latitude = (double)coords[0];
          result.Longitude = (double)coords[1];
          result.Success = true;
          result.Message = "Success";
        }
      }
      return result;

    }
  }
}
