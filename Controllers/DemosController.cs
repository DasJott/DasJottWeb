using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace DasJott.Controllers
{
    public class DemosController : BaseController
  {
    protected readonly ILogger<DemosController> Logger;
    public DemosController(ILogger<DemosController> log)
    {
      Logger = log;
    }

    public IActionResult Ajax()
    {
      Logger.LogVerbose("Ajax called");
      return View("Ajax");
    }

    public IActionResult Jquery()
    {
      Logger.LogVerbose("Jquery called");
      return View("NotYet");
    }

    public IActionResult Design()
    {
      Logger.LogVerbose("Design called");
      return View("NotYet");
    }
  }
}