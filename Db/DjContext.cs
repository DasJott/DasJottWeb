using System.IO;
using DasJott.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;

namespace DasJott.Database
{
  public class DjContext : DbContext
  {
    public DbSet<BlogEntry> BlogEntries { get; set; }
    public DbSet<NewsArticle> NewsArticles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var path = PlatformServices.Default.Application.ApplicationBasePath;
      optionsBuilder.UseSqlite("Filename=" + Path.Combine(path, "Db", "DasJott.db"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Entity>()
        .HasKey(e => e.ID)
        .HasName("ID");

      modelBuilder.Entity<Entity>()
        .Property(e => e.Created)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<Entity>()
        .Property(e => e.Updated)
        .ValueGeneratedOnAddOrUpdate();

      modelBuilder.Ignore<Entity>();
    }
  }
}
