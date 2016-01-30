using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DasJott.Interfaces.Services;
using DasJott.Services;
using DasJott.Database;
using Microsoft.AspNet.Mvc.Razor;

namespace DasJott
{
  public class Startup
  {
    private ModularViewLocator _modularViewLocations = new ModularViewLocator();

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddLogging();
      services.AddScoped<IBundleService, BundleService>();

      services.Configure<RazorViewEngineOptions>(opts =>
      {
        opts.ViewLocationExpanders.Add(_modularViewLocations);
      });

      services.AddEntityFramework()
      .AddSqlite()
      .AddDbContext<DjContext>();

      /*
      // Adds a pre-existing instance that will be referenced
      IServiceCollection AddInstance<TService>(TService implementationInstance);

      // Creates an instance once per request scope
      IServiceCollection AddScoped<TService, TImplementation>();

      // Creates a single instance that will be used each time the service is requested
      IServiceCollection AddSingleton<TService>();

      // Creates a new instance each time the service is requested
      IServiceCollection AddTransient<TService>();
      */
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      loggerFactory.MinimumLevel = LogLevel.Debug;
      loggerFactory.AddConsole(LogLevel.Verbose);

      _modularViewLocations.Logger = loggerFactory.CreateLogger("ModularViewLocator");

      app.UseIISPlatformHandler();
      app.UseDeveloperExceptionPage();

      //app.UseMvcWithDefaultRoute();
      app.UseMvc(route =>
      {
        route.MapRoute(
          "Default",
          "{controller}/{action}/{id}",
          new { controller = "Start", action = "Index", id = "" }
        );
      });

      app.UseStaticFiles();

    }
  }
}
