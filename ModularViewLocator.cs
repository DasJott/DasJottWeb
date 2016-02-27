using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Logging;

namespace DasJott
{
    public class ModularViewLocator : IViewLocationExpander
  {
    public ILogger Logger { get; set; }

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
      if (Logger != null) { Logger.LogVerbose("ExpandViewLocations called"); }

      // default:
      // "/Views/{1}/{0}.cshtml"
      // also:
      // 0 = action
      // 1 = controller

      var routes = new string[] {
        "/Sections/{1}/Views/{0}.cshtml",
        "/Sections/Common/Views/{0}.cshtml"
      };

      return routes;
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
  }

//   public class ModuleView : IDisposable
//   {
//     protected HtmlHelper Helper;
//
//     public ModuleView(HtmlHelper helper, string title)
//     {
//       Helper = helper;
//
//
//       var html = new StringBuilder();
//       html.AppendFormat("<div id='{0}'>", title);
//
//       Helper.ViewContext.Writer.Write(html.ToString());
//     }
//
//     public void Dispose()
//     {
//       var html = new StringBuilder();
//       html.Append("</div>");
//
//       Helper.ViewContext.Writer.Write(html.ToString());
//     }
//
//   }
}
