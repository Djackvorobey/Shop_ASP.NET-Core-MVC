using System.ComponentModel.DataAnnotations;

namespace TestProjectMVC.Models.VievModel
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            Product = new Product();
        }
        public Product Product { get; set; }
        public bool ExistsInCart { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Product Amount")]
        public int ProductAmount { get; set; }
    }
}
