using System.ComponentModel.DataAnnotations;
using TestProjectMVC.Data;

namespace TestProjectMVC.Models
{
    public class ApplicationType
    {
       
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
