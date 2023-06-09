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
    
    public ActionResult Create(int machineId = 0)
    {
      if (machineId > 0)
      {
        Machine selectedMachine = _db.Machines
          .FirstOrDefault(machine => machine.MachineId == machineId);
        return View(selectedMachine);
      }
      else if (_db.Engineers.ToList().Count == 0)
      {
        return RedirectToAction("Create","Engineers", new { error = "you need to add an engineer to the system before you can add a machine" });
      }
      else
      {
        return View();
      }
    }

    [HttpPost]
    public ActionResult Create(Machine newMachine, int id)
    {
      _db.Machines.Add(newMachine);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

    public ActionResult Details(int id, bool error = false)
    {
      ViewBag.Engineers = new SelectList(_db.Engineers,"EngineerId","First");
      Machine selectedMachine = _db.Machines
        .Include(machine => machine.JoinEntities)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(machine => machine.MachineId == id);
      if (error)
      {
        ViewBag.Error = "That engineer already is paired with this machine";
      }
      return View(selectedMachine);
    }

    [HttpPost]
    public ActionResult Details(Machine machine,int Engineers)
    {
      #nullable enable 
      EngineerMachine? JoinEntities = _db.EngineerMachines.FirstOrDefault(join => (join.EngineerId == Engineers && join.MachineId == machine.MachineId));
      #nullable disable
      if (JoinEntities == null)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = Engineers, MachineId = machine.MachineId } );
        _db.SaveChanges();
        return RedirectToAction("Details","Machines");
      }
      else
      {
        return RedirectToAction("Details","Machines", new { id = machine.MachineId, error = true });
      }
    }
    
    public ActionResult DeleteJoin(int id, int machineId)
    {
      EngineerMachine joinEntity = _db.EngineerMachines
        .FirstOrDefault(join => join.EngineerMachineId == id);
      _db.EngineerMachines.Remove(joinEntity);
      _db.SaveChanges();
      return RedirectToAction("Details","Machines", new { id = machineId });
    }

    public ActionResult Delete(int machineId)
    {
      Machine selectedMachine = _db.Machines
        .FirstOrDefault(machine => machine.MachineId == machineId);
      _db.Machines.Remove(selectedMachine);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

    public ActionResult Edit(Machine machine, int machineId)
    {
      machine.MachineId = machineId;
      _db.Machines.Update(machine);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

  }

}
