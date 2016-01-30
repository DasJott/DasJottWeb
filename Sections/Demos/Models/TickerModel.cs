using System.Collections.Generic;

namespace DasJott.Demos.ViewModels
{
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
}
