using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Linq;

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
        Engineer[] engineerList = _db.Engineers.ToArray();
        Machine[] machineList = _db.Machines.ToArray();
        return View(new { Engineers = engineerList, Machines = machineList});
      }
    }
}
