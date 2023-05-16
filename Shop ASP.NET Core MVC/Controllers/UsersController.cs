using Microsoft.AspNetCore.Mvc;
using TestProjectMVC.Data;
using TestProjectMVC.Models;

namespace TestProjectMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dB;
        public UsersController(ApplicationDbContext dB)
        {
            _dB = dB;
        }


        public IActionResult Index()
        {
            IEnumerable<Users> objList = _dB.Users;
            return View(objList);
        }

        //GET - Create
        public IActionResult Create()
        {

            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken] //токен захисту від взлома
        public IActionResult Create(Users obj)
        {
            obj.time = DateTime.Now;
            _dB.Users.Add(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Bootstrap()
        {
            return View();
        }
    }
}
