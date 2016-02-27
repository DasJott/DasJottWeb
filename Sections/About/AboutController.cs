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
      Logger.LogVerbose("Index called");
      return View("Index");
    }

    [Route("about/modal"), HttpGet]
    public IActionResult Modal()
    {
      Logger.LogVerbose("Modal called");
      Logger.LogVerbose("Setting DoModal to true and use Index view");
      ViewBag.DoModal = true;
      return View("Index");
    }
  }
}