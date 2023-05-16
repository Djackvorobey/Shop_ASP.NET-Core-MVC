using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestProjectMVC.Data;
using TestProjectMVC.Models;
using TestProjectMVC.Models.VievModel;


namespace TestProjectMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dB;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext dB, IWebHostEnvironment webHostEnvironment)
        {
            this._dB = dB;
            this._webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objList = _dB.Products.Include(u=> u.Category);
            
            return View(objList);
        }

        //GET - UPSERT
        public IActionResult Upsert(int? id) 
        {
            
           ProductVM productVM = new ProductVM()
           {
               Product = new Product(),
               CategorySelectedList = _dB.Category.Select(i => new SelectListItem
               {
                   Text= i.Name,
                   Value= i.Id.ToString()
               })
           };
            if (id == null)
            {
                //this is for create
                return View(productVM);
            }
            else
            {
                productVM.Product = _dB.Products.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
           
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {
                    //Creating
                    string upload = webRootPath + WebConstants.ImagePath;
                    string filename = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload,filename+extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.Image = filename + extension;

                    _dB.Products.Add(productVM.Product);
                    
                }
                else
                {
                    //Updating

                    var objFromDB = _dB.Products.AsNoTracking().FirstOrDefault(i=> i.Id == productVM.Product.Id);

                    if (files.Count > 0 )
                    {
                        string upload = webRootPath + WebConstants.ImagePath;
                        string filename = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        
                        var oldFile = Path.Combine(upload,objFromDB.Image);
                        
                    
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = filename + extension;
                    }
                    else
                    {
                    productVM.Product.Image = objFromDB.Image;
                    }
                    _dB.Products.Update(productVM.Product);
                }
                _dB.SaveChanges();
            
            return RedirectToAction("Index");
        }
        
        //DELETE 

        public IActionResult Delete(int id)
        {
            if (id !=0)
            {
                var product = _dB.Products.FirstOrDefault(i => i.Id == id);

                string webRootPath = _webHostEnvironment.WebRootPath;

                var upload = webRootPath + WebConstants.ImagePath;

                var imageFile = Path.Combine(upload, product.Image);

                System.IO.File.Delete(imageFile);

                _dB.Products.Remove(product);

                _dB.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
