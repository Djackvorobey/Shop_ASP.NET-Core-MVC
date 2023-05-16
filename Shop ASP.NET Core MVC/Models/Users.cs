using System.ComponentModel.DataAnnotations;

namespace TestProjectMVC.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime time { get; set; } 
    }
}
