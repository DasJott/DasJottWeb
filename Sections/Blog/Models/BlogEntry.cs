using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DasJott.Database;

namespace DasJott.Blog.Models
{
  public class BlogEntry : Entity
  {
    [Required]
    [MinLength(4)]
    [Column(TypeName = "varchar(20)")]
    public string Headline { get; set; }

    [Required]
    [MinLength(4)]
    [Column(TypeName = "varchar(4000)")]
    public string Text { get; set; }
  }
}
