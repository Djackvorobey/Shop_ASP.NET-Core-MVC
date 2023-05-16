namespace TestProjectMVC.Models.VievModel
{
    public class HomeVM
    {
        public IEnumerable <Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
