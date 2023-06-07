using Microsoft.AspNetCore.Mvc;
using TestProjectMVC.Models;
using TestProjectMVC.Utility;
using TestProjectMVC.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TestProjectMVC.Models.VievModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestProjectMVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart)!=null 
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                // session exist
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            // List<int> prodInCart = shoppingCartsList.Select(i=>i.ProductId).ToList();

            // IEnumerable<ProductVM> prodList = _db.Products.Where(u => prodInCart.Contains(u.Id));

            //var result = shoppingCartsList.Select(item => new { ProductId = item.ProductId, ProductAmount = item.ProductAmount });
            IEnumerable<ProductVM> result = (from cartItem in shoppingCartsList
                          join product in _db.Products on cartItem.ProductId equals product.Id
                         
                          select new ProductVM
                          {
                              Product = product,
                              Amount = cartItem.ProductAmount
                          }).ToList();

         

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(int[] Amount)
        {

            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                // session exist
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }

            for (int i = 0; i < Amount.Length; i++)
            {
                if (Amount[i] != shoppingCartsList[i].ProductAmount)
                {
                    Amount[i] = shoppingCartsList[i].ProductAmount;
                }
            }
            
                
                    
                
            

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartsList);


            return RedirectToAction(nameof(Summary)); 
        }

        
        public async Task<IActionResult> Summary()
        {
           
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                // session exist
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            List<int> prodInCart = shoppingCartsList.Select(i => i.ProductId).ToList();

            IEnumerable<Product> prodList = _db.Products.Where(u => prodInCart.Contains(u.Id));

            List<ProductVM> productsVM = new();
            
            List<Product> products = new(prodList);

            

            for (int i = 0; i < prodList.Count(); i++)
            {
                productsVM.Add(new ProductVM { Product = products[i], Amount = shoppingCartsList[i].ProductAmount });
               
            }



            var userManager = HttpContext.RequestServices.GetService<UserManager<Users>>();
            var loggedInUser = HttpContext.User;

            
                var userName = loggedInUser.Identity.Name;

            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                ProductUserVM = new ProductUserVM()
                {
                    ApplicationUser = user,
                    ProductList = productsVM
                };
                // Знайдено користувача в базі даних
                // Виконуйте додаткові дії зі знайденим користувачем за потреби
            }
            else
            {
                // Користувача не знайдено в базі даних
                // Обробка відсутності користувача
                return NotFound();
            }

            return View(ProductUserVM);
        }
        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                // session exist
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }

            shoppingCartsList.Remove(shoppingCartsList.FirstOrDefault(u=>u.ProductId == id));

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartsList);
            return RedirectToAction("Index");
        }
    }
}
