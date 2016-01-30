using Microsoft.AspNet.Mvc.Rendering;

namespace DasJott.Interfaces.Services {
  public interface IBundleService {
    void IncludeStyle(string bundleName);
    void IncludeScript(string bundleName);
    void IncludeBundle(string bundleName);
    HtmlString RenderScript();
    HtmlString RenderStyle();
  }
}
