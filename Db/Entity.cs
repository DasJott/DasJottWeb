using System;

namespace DasJott.Database
{
  public abstract class Entity
  {
    public int ID { get; set; }

    /// is automaticly set
    public DateTime Created { get; set; }
    /// is automaticly set
    public DateTime Updated { get; set; }
    public bool Deleted { get; set; }
    internal virtual void OnCreate() {}
    internal virtual void OnUpdate() {}
  }
}
