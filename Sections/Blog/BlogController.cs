using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using DasJott.Blog.Models;
using System.Collections.Generic;
using DasJott.Common.Controllers;

namespace DasJott.Blog
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
