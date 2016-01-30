using System.Linq;
using DasJott.Common.Controllers;
using DasJott.Demos.ViewModels;
using DasJott.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace DasJott.Demos
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

    #region AsyncRequestActions

    [Route("demos/newsticker")]
    public IActionResult Newsticker(int? num)
    {
      var model = new TickerModel();

      using (var log = Logger.BeginScope("Getting newsticker using WebService")) {
        var web = new WebService(Logger);

        string tickerText = null;
        var doc = web.RequestHtml("http://www.der-postillon.com/search/label/Newsticker");
        if (doc != null) {
          int tickernum = num ?? 0;
          var tickerTexts = doc.DocumentNode.Descendants("div")
            .Where(div => div.Attributes["class"] != null && div.Attributes["class"].Value == "post-body entry-content")
            .Select(div => div.InnerText).ToArray();
          tickerText = tickerTexts[tickernum];
        }

        string[] lines = null;
        if (tickerText != null) {
          tickerText = tickerText.Trim('"');
          Logger.LogVerbose("Seems we have got some newsticker text");
          lines = tickerText.Split('\n');
          Logger.LogVerbose("text contains {0} lines", lines.Length);
        }

        if (lines != null && lines.Length > 0) {
          foreach (var line in lines) {
            if (line != "" && line.StartsWith("+")) {
              model.News.Add(line);
            }
          }
        }
      }

      return View(model);
    }

    #endregion AsyncRequestActions
  }
}