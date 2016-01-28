using System;

namespace DasJott.Database
{
  public abstract class Entity
  {
    public int ID { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    internal virtual void OnCreate()
    {
      Created = DateTime.Now;
    }
    internal virtual void OnUpdate()
    {
      Updated = DateTime.Now;
    }
  }
}
