using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Linq;
using Services;

namespace DasJott.Backend.Services {
  public class BundleService : IBundleService {
    public enum RenderType {
      STYLES, SCRIPTS
    }

    protected static string bundleResult = Path.Combine("Content", "bundle.result.json");
    
    private readonly IApplicationEnvironment _appEnvironment;
    private readonly Dictionary<RenderType, List<string>> _toBeRendered = new Dictionary<RenderType, List<string>>();
    private JObject _jObject = null;
    private readonly ILogger _logger = null;

    public BundleService(IApplicationEnvironment appEnvironment, ILoggerFactory loggerFactory)
    {
      _appEnvironment = appEnvironment;
      _logger = loggerFactory.CreateLogger("BundleService");
      _logger.LogDebug("Application base path: \"{0}\"", _appEnvironment.ApplicationBasePath);
      _logger.LogInformation("BundleService created");
    }

    protected JObject GetBundleResult()
    {
      if (_jObject == null) {
        string fileName = Path.Combine(_appEnvironment.ApplicationBasePath, bundleResult);
        string jsonText = File.ReadAllText(fileName);
        _jObject = JObject.Parse(jsonText);
      }
      return _jObject;
    }

    protected HtmlString GetObject(string bundleName, RenderType t) {
      string type = "";
      switch (t) {
        case RenderType.SCRIPTS:
        type = "scripts";
        break;
        case RenderType.STYLES:
        type = "styles";
        break;
      }

      string path = string.Format("{0}.{1}", bundleName, type);
      JObject jSon = GetBundleResult();
      JToken token = jSon.SelectToken(path);
      if (token != null) {
        return new HtmlString(token.ToString());
      }
      
      return null;
    }
    
    protected virtual void Include(RenderType key, string bundleName) {
      _logger.LogVerbose("Include called with parameters: {0} {1}", key.ToString(), bundleName);
      if (!_toBeRendered.ContainsKey(key)) {
        _logger.LogDebug(" first time adding of type {0}", key.ToString());
        var list = new List<string>();
        _toBeRendered.Add(key, list);
      }
      _toBeRendered[key].Insert(0, bundleName);
      _logger.LogVerbose("Include added {0} to key {0}", bundleName, key.ToString());
    }
    
    protected virtual HtmlString Render(RenderType key) {
      _logger.LogVerbose("Render called with option {0}", key.ToString());
      var sb = new StringBuilder();

      if (_toBeRendered.ContainsKey(key)) {
        foreach (var bundleName in _toBeRendered[key]) {
          var bundleData = GetObject(bundleName, key);
          if (bundleData != null) {
            sb.AppendLine(bundleData.ToString());
          }
        }
      } else {
        _logger.LogDebug(" no bundle data contained");
      }

      _logger.LogVerbose("Render outputs: {0}", sb.ToString());
      return new HtmlString(sb.ToString());
    }

    public void IncludeStyle(string bundleName)
    {
      Include(RenderType.STYLES, bundleName);
    }

    public void IncludeScript(string bundleName)
    {
      Include(RenderType.SCRIPTS, bundleName);
    }
    
    public void IncludeBundle(string bundleName)
    {
      IncludeStyle(bundleName);
      IncludeScript(bundleName);
    }

    public HtmlString RenderScript()
    {
      return Render(RenderType.SCRIPTS);
    }

    public HtmlString RenderStyle()
    {
      return Render(RenderType.STYLES);
    }
  }
}