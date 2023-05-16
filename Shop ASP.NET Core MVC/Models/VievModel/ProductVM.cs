using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestProjectMVC.Models.VievModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategorySelectedList { get; set; }
    }
}
