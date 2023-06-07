
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProjectMVC.Models;
using TestProjectMVC.Utility;
using System.Diagnostics;
using TestProjectMVC.Data;
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
           
            // Код для извлечения сесии
            List<ShoppingCart> shoppingCartList = new();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() >= 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            //Код для извлечения сесии

            DetailsVM detailsVM = new()
            {
                Product = _dB.Products.Include(u => u.Category).Where(u => u.Id == Id).FirstOrDefault(),
                ExistsInCart = false
            };

            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == Id)
                {
                    detailsVM.ExistsInCart = true;
                }
            }

            return View(detailsVM);
        }


        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(DetailsVM detailsVM)
        {

            // Код для извлечения сесии
            List<ShoppingCart> shoppingCartList = new();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart)!= null 
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() >=0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            //Код для извлечения сесии

            shoppingCartList.Add(new ShoppingCart() { ProductId= detailsVM.Product.Id, ProductAmount = detailsVM.ProductAmount});
            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult RemoveFromCart(int Id)
        {
            // Код для извлечения сесии
            List<ShoppingCart> shoppingCartList = new();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() >= 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            //Код для извлечения сесии

            var itemToRemove = shoppingCartList.SingleOrDefault(u=>u.ProductId== Id);
            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Index));
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