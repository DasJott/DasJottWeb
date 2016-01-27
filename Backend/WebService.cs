using System.Net;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace DasJott.Backend.Services {
  public class WebService {
    
    protected readonly ILogger Logger;
    
    public WebService(ILogger logger)
    {
      Logger = logger;
    }

    public HtmlDocument RequestHtml(string url)
    {
      using (var log = Logger.BeginScope("WebService.RequestHtml")) {
        HtmlDocument doc = new HtmlDocument();
        
        using (var client = new WebClient())
        {
          Logger.LogVerbose("Using WebClient on url: \"{0}\"", url);
          var bytes = client.DownloadData(url);
          Logger.LogVerbose("{0} bytes received", bytes.Length);
          var html = client.Encoding.GetString(bytes);
          doc.LoadHtml(html);
        }

        return doc;
      }
    }
  }
}