using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProjectMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }
        [Range(1,double.MaxValue)]
        public double Price { get; set; }
        [MaxLength(5000)]
        public string Image { get; set; }
        [Display(Name="Category Type")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
