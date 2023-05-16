using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestProjectMVC.Data;
using TestProjectMVC.Models;

namespace TestProjectMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dB;

        public CategoryController(ApplicationDbContext dB)
        {
            _dB = dB;
        }
        //[Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            //var user = HttpContext.User;
           
            //var email = user.FindFirst(ClaimTypes.Email)?.Value;
            //var name = user.FindFirst(ClaimTypes.Name)?.Value;
           
            IEnumerable<Category> objList = _dB.Category;
            
            return View(new { objList = objList }); //email = email, user = name
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }
        
        public IActionResult RedirectUrl()
        {
            return View();
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken] //токен захисту від взлома
        public IActionResult Create(Category obj)
        {
                _dB.Category.Add(obj);
                _dB.SaveChanges();
                return RedirectToAction("Index");
           
            // Validation on server side
            //if (ModelState.IsValid)
            //{
            //    _dB.Category.Add(obj);
            //    _dB.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(obj);

        }
        // GET - Edit
        
        public IActionResult Edit(int id)
        {
            if (id == null || id==0){ return NotFound();}

            var obj = _dB.Category.Find(id);

            if (obj == null) { return NotFound(); }

            return View(obj);
        }
        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken] //токен захисту від взлома
        public IActionResult Edit(Category obj)
        {
            _dB.Category.Update(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int? id)
        {
            var obj = _dB.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dB.Category.Remove(obj);
            _dB.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
