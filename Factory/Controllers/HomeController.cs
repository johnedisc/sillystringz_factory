using Microsoft.AspNetCore.Mvc;
using Factory.Models;

namespace Factory.Controllers
{
    public class HomeController : Controller
    {
      private readonly FactoryContext _db;

      public HomeController(FactoryContext db)
      {
        _db = db;
      }

      public ActionResult Index()
      {
        Engineer[] engineerList = _db.Engineers.ToList();
        Machine[] machineList = _db.Machines.ToList();
        return View(new { Engineers = engineerList, Machines = machineList});
      }
    }
}
