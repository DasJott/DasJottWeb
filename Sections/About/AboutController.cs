using DasJott.Common.Controllers;
using DasJott.Database;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace DasJott.About
{
  public class AboutController : BaseController
  {
    protected readonly ILogger<AboutController> Logger;
    protected DjContext db;

    public AboutController(ILogger<AboutController> log, DjContext context)
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