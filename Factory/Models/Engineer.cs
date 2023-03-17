using System.Collections.Generic;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    public string First { get; set; }
    public string Last { get; set; }
    public List<EngineerMachine> JoinEntities { get;}
  }
}

