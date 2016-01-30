using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DasJott.Database;

namespace DasJott.Common.Models
{
  public class User : Entity
  {
    [Required]
    [MinLength(4)]
    [Column(TypeName = "varchar(20)")]
    public string Name { get; set; }

    [Required]
    [MinLength(4)]
    [Column(TypeName = "varchar(100)")]
    public string ShortInfo { get; set; }

    [Required]
    public DateTime Birthday { get; set; }
  }
}
