using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestProjectMVC.Data;
using TestProjectMVC.Models;
using TestProjectMVC.Models.VievModel;

namespace TestProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dB;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dB)
        {
            _logger = logger;
            _dB = dB;
        }

        public IActionResult Index()
        {
            var Products1 = _dB.Products.Include(u => u.Category);

            HomeVM homeVM = new()
            {
                Products = _dB.Products.Include(u => u.Category),
                Categories = _dB.Category
            };
            return View(homeVM);
        }

        public IActionResult Details(int Id)
        {
            DetailsVM detailsVM = new()
            {
                Product = _dB.Products.Include(u => u.Category).Where(u => u.Id == Id).FirstOrDefault(),
                ExistsInCart = false
            };

            return View(detailsVM);
        }
            
        
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}