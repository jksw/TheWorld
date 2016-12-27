using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using TheWorld.ViewModels;

namespace TheWorld
{
  public class Startup
  {
    private IHostingEnvironment _env;
    private IConfigurationRoot _config;

    public Startup(IHostingEnvironment env)
    {
      _env = env;

      //config.json is from the Completing the Form module in the SWan W Pluralsight class
      //purpose is to store fields used in the email process
      var builder = new ConfigurationBuilder()
        .SetBasePath(_env.ContentRootPath)
        .AddJsonFile("config.json")
        .AddEnvironmentVariables();


      _config = builder.Build();

    }



    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddSingleton(_config);

      //Our own services
      if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
      {
        services.AddScoped<IMailService, DebugMailService>();
      }
      else
      {
        // Implement real service
      }

      services.AddDbContext<WorldContext>();

      services.AddScoped<IWorldRepository, WorldRepository>();

      services.AddTransient<WorldContextSeedData>();

      services.AddLogging();


      //Need services to make mvc work
      //services.AddMvc();

      services.AddMvc()
      .AddJsonOptions(config =>
      {
        config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app,
        IHostingEnvironment env,
        ILoggerFactory factory,
        WorldContextSeedData seeder)
    {

      //Automapper
      //TODO I do not understand how this statement works.  What is config?
      Mapper.Initialize(config =>
      {
        config.CreateMap<TripViewModel, Trip>().ReverseMap();
      });


      //Only needed when tutorial started -- because the tutorial started with a static
      // html file.  After adding View, UseDefaultFiles() is preventing the view from loading.
      //app.UseDefaultFiles();
      app.UseStaticFiles();

      if (env.IsEnvironment("Development"))
      {
        app.UseDeveloperExceptionPage();

        factory.AddDebug(LogLevel.Information);
      }
      else
      {
        factory.AddDebug(LogLevel.Error);
      }




      //After adding view, need to enable route to the view
      app.UseMvc(config =>
        {
          config.MapRoute(
            name: "Default",
            template: "{controller}/{action}/{id?}",
            defaults: new { controller = "App", action = "Index" }
            );
        });

      seeder.EnsureSeedData().Wait();



      //loggerFactory.AddConsole();

      //if (env.IsDevelopment())
      //{
      //    app.UseDeveloperExceptionPage();
      //}

      //app.Run(async (context) =>
      //{
      //    await context.Response.WriteAsync("Hello World!");
      //});
    }
  }
}
