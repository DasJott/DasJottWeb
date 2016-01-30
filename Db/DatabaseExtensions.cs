using System.Collections.Generic;
using System.Linq;
using DasJott.Common.Models;
using Microsoft.Data.Entity;

namespace DasJott.Database.Extensions
{
  public static class DatabaseExtensions
  {
    #region generics
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

    #endregion generics

    #region settings
    // get settings
    public static string GetString(this DbSet<Settings> table, string key)
    {
      return (from s in table where s.Key == key select s.Value).FirstOrDefault() ?? "";
    }
    public static int GetInt(this DbSet<Settings> table, string key)
    {
      int ret = 0;
      var Value = (from s in table where s.Key == key select s.Value).FirstOrDefault();
      if (Value == null || !int.TryParse(Value, out ret)) {
        return 0;
      }
      return ret;
    }
    public static double GetDouble(this DbSet<Settings> table, string key)
    {
      double ret = 0.0;
      var Value = (from s in table where s.Key == key select s.Value).FirstOrDefault();
      if (Value == null || !double.TryParse(Value, out ret)) {
        return 0.0;
      }
      return ret;
    }

    #endregion settings
  }
}
