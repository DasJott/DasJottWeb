using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using DasJott.Models;

namespace DasJott.Controllers
{
  public class BlogController : BaseController
  {
    protected readonly ILogger<BlogController> Logger;
    public BlogController(ILogger<BlogController> log)
    {
      Logger = log;
    }

    public IActionResult Index()
    {
      Logger.LogVerbose("Index called");
      
      var entry = new BlogEntry() {
        Headline = "",
        Text = "",
        Date = "",
        Next = 1,
        Previous = 0
      };
      
      return View(entry);
    }
  }
}
