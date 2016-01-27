using System.ComponentModel.DataAnnotations;

namespace DasJott.Models
{
  public class User : Entity
  {
    [Required]
    [MinLength(4)]
    public string Name { get; set; }
    [Required]
    [MinLength(10)]
    public string ShortInfo { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
  }
}
