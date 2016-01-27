using System.Collections.Generic;

namespace DasJott.Models
{
  public class HomeContent
  {
    public List<Item> Articles { get; set; }
    public class Item
    {
      public string Headline { get; set; }
      public string Content { get; set; }
    }
  }
}
