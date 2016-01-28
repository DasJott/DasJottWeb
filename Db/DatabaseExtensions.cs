using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace DasJott.Database.Extensions
{
  public static class DatabaseExtensions
  {
    /// get list of all items
    public static IQueryable<T> GetList<T>(this DbSet<T> table, bool showDeleted=false) where T : Entity
    {
      return (from x in table where showDeleted || !x.Deleted select x);
    }

    /// get list of certain ids
    public static IQueryable<T> GetListForIds<T>(this DbSet<T> table, IEnumerable<int> ids, bool showDeleted = false) where T : Entity
    {
      return (from x in table where (showDeleted || !x.Deleted) && ids.Contains(x.ID) select x);
    }
  }
}
