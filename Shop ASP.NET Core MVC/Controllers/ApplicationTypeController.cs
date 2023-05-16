using Microsoft.AspNetCore.Mvc;
using TestProjectMVC.Data;
using TestProjectMVC.Models;

namespace TestProjectMVC.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _dB;

        public ApplicationTypeController(ApplicationDbContext dB)
        {
            _dB = dB;
        }
        public IActionResult Index()
        {

            IEnumerable<ApplicationType> objList = _dB.ApplicationType;

            return View(new { objList = objList }); //,email = email, user = name
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken] //токен захисту від взлома
        public IActionResult Create(ApplicationType obj)
        {
            _dB.ApplicationType.Add(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET - Edit

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0) { return NotFound(); }

            var obj = _dB.ApplicationType.Find(id);

            if (obj == null) { return NotFound(); }

            return View(obj);
        }
        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken] //токен захисту від взлома
        public IActionResult Edit(ApplicationType obj)
        {
            _dB.ApplicationType.Update(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int? id)
        {
            var obj = _dB.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dB.ApplicationType.Remove(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
