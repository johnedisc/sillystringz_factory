using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Linq;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }
    
    public ActionResult Create()
    {
      if (_db.Engineers.ToList().Count == 0)
      {
        return RedirectToAction("Create","Engineers");
      }
      else
      {
        ViewBag.Engineers = new SelectList(_db.Engineers, "EngineerId", "First");
        return View();
      }
    }

    [HttpPost]
    public ActionResult Create(Machine newMachine, int id)
    {
      #nullable enable
      EngineerMachine? JoinEntities = _db.EngineerMachines.FirstOrDefault(join => (join.EngineerId == id && join.MachineId == newMachine.MachineId));
      #nullable disable
      if (JoinEntities == null)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = id, MachineId = newMachine.MachineId } );
        _db.Machines.Add(newMachine);
        _db.SaveChanges();
        return RedirectToAction("Index","Home");
      }
      else
      {
        return RedirectToAction("Index","Home");
      }
    }

    public ActionResult Details(int id)
    {
      ViewBag.Engineers = new SelectList(_db.Engineers,"EngineerId","Name");
      Machine selectedMachine = _db.Machines
        .FirstOrDefault(machine => machine.MachineId == id);
      return View(selectedMachine);
    }
  }
}
