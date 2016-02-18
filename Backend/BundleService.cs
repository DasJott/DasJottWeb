using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Linq;
using DasJott.Interfaces.Services;

namespace DasJott.Services {
  public class BundleService : IBundleService {
    public enum RenderType {
      STYLES, SCRIPTS
    }
    public string RenderTypeString(RenderType t)
    {
      switch (t) {
        case RenderType.SCRIPTS: return "scripts";
        case RenderType.STYLES: return "styles";
      }
      return "";
    }

    protected static string bundleResult = Path.Combine(".", "bundle.result.json");
    //protected static string bundleResult = Path.Combine(".", "bundle.result.json");

    private readonly IApplicationEnvironment _appEnvironment;
    private readonly ILogger<BundleService> _logger = null;

    private readonly Dictionary<RenderType, List<string>> _toBeRendered = new Dictionary<RenderType, List<string>>();
    private List<string> _includeOrder = null;
    private JObject _jObject = null;

    public BundleService(IApplicationEnvironment appEnvironment, ILogger<BundleService> logger)
    {
      _appEnvironment = appEnvironment;
      _logger = logger;
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

    protected JToken GetBundleResultToken(string path)
    {
      JObject jSon = GetBundleResult();
      return jSon.SelectToken(path);
    }

    protected string GetHtmlInclude(string bundleName, RenderType t) {
      string type = RenderTypeString(t);

      string path = string.Format("{0}.{1}", bundleName, type);
      JToken token = GetBundleResultToken(path);
      if (token != null) {
        return token.ToString();
      }

      return null;
    }

    protected List<string> GetOrderList()
    {
      // one day we will get the order from the bundle result as well
      // but for now, we need to hack a little

      if (_includeOrder == null) {
        _includeOrder = new List<string>();
        _logger.LogVerbose("Creating order list");

        string fileName = Path.Combine(_appEnvironment.ApplicationBasePath, "bundle.order");
        string[] lines = File.ReadAllLines(fileName);

        foreach (var bundle in lines) {
          if (!string.IsNullOrEmpty(bundle)) {
            _includeOrder.Add(bundle.Trim());
          }
        }
        _logger.LogVerbose("Copied {0} bundle names", _includeOrder.Count);
      }

      return _includeOrder;
    }

    protected virtual void Include(RenderType key, string bundleName) {
      _logger.LogVerbose("Include called with parameters: {0} {1}", key.ToString(), bundleName);
      if (!_toBeRendered.ContainsKey(key)) {
        _logger.LogDebug(" first time adding of type {0}", key.ToString());
        var list = new List<string>();
        _toBeRendered.Add(key, list);
      }
      if (!_toBeRendered[key].Contains(bundleName)) {
        _toBeRendered[key].Add(bundleName);
        _logger.LogVerbose("Include added {0} to key {0}", bundleName, key.ToString());
      } else {
        _logger.LogInformation("Include was already added {0} to key {0}", bundleName, key.ToString());
      }
    }

    protected virtual HtmlString Render(RenderType key) {
      _logger.LogVerbose("Render called with option {0}", key.ToString());
      var sb = new StringBuilder();

      if (_toBeRendered.ContainsKey(key)) {

        // go through the order of bundles within the config file
        foreach (var bundle in GetOrderList()) {
          _logger.LogVerbose(" trying bundle {0}", bundle);
          if (_toBeRendered[key].Contains(bundle.ToString())) {
            _logger.LogVerbose(" rendering {0}", bundle);
            var html = GetHtmlInclude(bundle, key);
            if (html != null) {
              sb.AppendLine(html);
            }
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
