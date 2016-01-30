using System.ComponentModel.DataAnnotations.Schema;
using DasJott.Database;

namespace DasJott.News.Models
{
  public class NewsArticle : Entity
  {
    [Column(TypeName = "varchar(20)")]
    public string Headline { get; set; }

    [Column(TypeName = "varchar(2000)")]
    public string Content { get; set; }
  }
}
