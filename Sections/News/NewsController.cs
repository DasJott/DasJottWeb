using Microsoft.AspNet.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;
using DasJott.Database;
using DasJott.Database.Extensions;
using DasJott.News.Models;
using DasJott.Common.Controllers;

namespace DasJott.News
{
    public class NewsController : BaseController
  {
    protected readonly ILogger<NewsController> Logger;
    protected DjContext db;

    public NewsController(ILogger<NewsController> log, DjContext context)
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

  }
}
