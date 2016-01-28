using System;
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
        .ForSqliteHasName("ID");

      modelBuilder.Ignore<Entity>();
    }
    
    public override int SaveChanges()
    {
      foreach (var e in this.ChangeTracker.Entries<Entity>()) {
        switch (e.State)
        {
          case EntityState.Modified:
            e.Entity.Updated = DateTime.Now;
            e.Entity.OnUpdate();
            break;
          case EntityState.Added:
            e.Entity.Updated = DateTime.Now;
            e.Entity.Created = DateTime.Now;
            e.Entity.Deleted = false;
            e.Entity.OnCreate();
            break;
        }
      }
      return base.SaveChanges();
    }
    
  }
}
