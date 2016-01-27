using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DasJott.Backend.Services;
using DasJott.Common.Services;
using DasJott.Database;

namespace HelloMvc
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddLogging();
      services.AddScoped<IBundleService, BundleService>();
      
      services.AddEntityFramework()
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
      
      app.UseIISPlatformHandler();
      app.UseDeveloperExceptionPage();
      app.UseMvcWithDefaultRoute();
      app.UseStaticFiles();
      //app.UseWelcomePage();
    }
  }
}