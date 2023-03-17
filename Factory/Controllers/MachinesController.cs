using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
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
      Engineer selectedEngineer = _db.Engineers
        .FirstOrDefault(engineer => engineer.EngineerId = id);
      formData
      _db.Machines.Add(formData);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

    public ActionResult Details(int id)
    {
      Machine selectedMachine = _db.Machines
        .Include(machine => machine.JoinEntities)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(mac => mac.MachineId == id);
      return View(selectedMachine);
    }
  }
}
