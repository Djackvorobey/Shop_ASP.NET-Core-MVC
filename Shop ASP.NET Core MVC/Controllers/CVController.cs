using Microsoft.AspNetCore.Mvc;

namespace TestProjectMVC.Controllers
{
    public class CVController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
