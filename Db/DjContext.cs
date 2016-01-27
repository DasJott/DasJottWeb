using System.IO;
using DasJott.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;

namespace DasJott.Database
{
  public class DjContext : DbContext
  {
    public DbSet<BlogEntry> BlogEntries { get; set; }
    public DbSet<HomeContent> HomeContents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var path = PlatformServices.Default.Application.ApplicationBasePath;
      optionsBuilder.UseSqlite("Filename=" + Path.Combine(path, "Db", "DasJott.db"));
    }
  }
}
