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
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine formData)
    {
      _db.Machines.Add(formData);
      _db.SaveChanges();
      return RedirectToAction("Index","Home");
    }

    public ActionResult Details(int id)
    {
      Machine selectedMachine = _db.Machines
        .Include(join => join.JoinEntities)
        .ThenInclude(eng => eng.Engineer)
        .FirstOrDefault(mac => mac.MachineId == id);
      return View(selectedMachine);
    }
  }
}
