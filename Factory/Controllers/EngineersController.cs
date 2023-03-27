using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Create(int engineerId = 0, string error = "")
    {
      if (engineerId > 0)
      {
        Engineer selectedEngineer = _db.Engineers
          .FirstOrDefault(engineer => engineer.EngineerId == engineerId);
        return View(selectedEngineer);
      }
      else
      {
        ViewBag.Error = error;
        return View();
      }
    }

    [HttpPost]
    public ActionResult Create(Engineer formData)
    {
      _db.Engineers.Add(formData);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

    public ActionResult Details(int id, bool error = false)
    {
      ViewBag.Machines = new SelectList(_db.Machines,"MachineId","Name");
      Engineer selectedEngineer = _db.Engineers
        .Include(engineer => engineer.JoinEntities)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      if (error)
      {
        ViewBag.Error = "That machine already is paired with this engineer";
      }
      return View(selectedEngineer);
    }

    [HttpPost]
    public ActionResult Details(Engineer engineer,int Machines)
    {
      #nullable enable 
      EngineerMachine? JoinEntities = _db.EngineerMachines.FirstOrDefault(join => (join.MachineId == Machines && join.EngineerId == engineer.EngineerId));
      #nullable disable
      if (JoinEntities == null)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { MachineId = Machines, EngineerId = engineer.EngineerId } );
        _db.SaveChanges();
        return RedirectToAction("Details","Engineers");
      }
      else
      {
        return RedirectToAction("Details","Engineers", new { id = engineer.EngineerId, error = true });
      }
    }
    
    public ActionResult DeleteJoin(int id, int engineerId)
    {
      EngineerMachine joinEntity = _db.EngineerMachines
        .FirstOrDefault(join => join.EngineerMachineId == id);
      _db.EngineerMachines.Remove(joinEntity);
      _db.SaveChanges();
      return RedirectToAction("Details","Engineers", new { id = engineerId });
    }

    public ActionResult Delete(int engineerId)
    {
      Engineer selectedEngineer = _db.Engineers
        .FirstOrDefault(engineer => engineer.EngineerId == engineerId);
      _db.Engineers.Remove(selectedEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

    public ActionResult Edit(Engineer engineer, int engineerId)
    {
      engineer.EngineerId = engineerId;
      _db.Engineers.Update(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

  }
}
