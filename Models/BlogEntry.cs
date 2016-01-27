using System.ComponentModel.DataAnnotations;

namespace DasJott.Models
{
  public class BlogEntry
  {
    [Required]
    [MinLength(4)]
    public string Headline { get; set; }
    [Required]
    [MinLength(4)]
    public string Text { get; set; }
    public string Date { get; set; }
    public int Next { get; set; }
    public int Previous { get; set; }
  }
}
