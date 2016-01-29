using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using DasJott.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using DasJott.Backend.Services;
using DasJott.Database;
using DasJott.Database.Extensions;
using System;
using DasJott.ViewModels;

namespace DasJott.Controllers
{
  public class HomeController : BaseController
  {
    protected readonly ILogger<HomeController> Logger;
    protected DjContext db;

    public HomeController(ILogger<HomeController> log, DjContext context)
    {
      Logger = log;
      db = context;
    }

    public IActionResult Index()
    {
      Logger.LogVerbose("Index called");

      var content = new HomeContent();
      content.Articles = db.NewsArticles.GetList().OrderByDescending(x => x.Updated).ToList();

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
      var birth = DateTime.Parse("1979-10-15");
      User user = new User()
      {
        Name = "Das Jott",
        ShortInfo = "Die Welt des zehnten Buchstaben aus dem Alphabet",
        Birthday = birth
      };

      return user;
    }
  }
}