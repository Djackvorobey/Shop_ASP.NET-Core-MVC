namespace TestProjectMVC.Models.VievModel
{
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList= new List<ProductVM>();   
        }
        public Users ApplicationUser { get; set; }
        public IEnumerable<ProductVM> ProductList { get; set; }

        
    }
}
