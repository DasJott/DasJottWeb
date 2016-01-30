using DasJott.Common.Controllers;
using DasJott.Database;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace DasJott.Start.Controllers
{
  public class StartController : BaseController
  {
    protected readonly ILogger<StartController> Logger;
    protected DjContext db;

    public StartController(ILogger<StartController> log, DjContext context)
    {
      Logger = log;
      db = context;
    }

    public IActionResult Index()
    {
      return View();
    }
  }
}