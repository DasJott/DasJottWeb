using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using DasJott.Models;
using System.Collections.Generic;

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

      var content = new BlogContent();
      
      return View(content);
    }
  }
  
  public class BlogContent
  {
    public List<BlogEntry> Articles { get; set; }
  }
}
