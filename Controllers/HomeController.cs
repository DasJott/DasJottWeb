using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using DasJott.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using Backend.Services;

namespace DasJott.Controllers
{
    public class HomeController : BaseController
  {
    protected readonly ILogger<HomeController> Logger;
    public HomeController(ILogger<HomeController> log)
    {
      Logger = log;
    }
    public IActionResult Index()
    {
      Logger.LogVerbose("Index called");
      
      var content = new HomeContent() {
        Articles = new List<HomeContent.Item>()
      };

      return View(content);
    }

    [Route("home/newsticker")]
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

    public class TickerModel
    {
      public TickerModel()
      {
        News = new List<string>();
      }
      public TickerModel(string[] list)
      {
        if (list != null) {
          News = new List<string>(list);
        } else {
          News = new List<string>();
        }
      }
      public List<string> News { get; set; }
    }

    public User CreateUser()
    {
      User user = new User()
      {
        Name = "Das Jott",
        ShortInfo = "Die Welt des zehnten Buchstaben aus dem Alphabet",
        Address = "Home sweet home"
      };

      return user;
    }
  }
}