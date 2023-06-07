using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TestProjectMVC.Data;

namespace TestProjectMVC.Models
{
    public class Users: IdentityUser
    {
        
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Region { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string PostalCode { get; set; }


       
       
        public DateTime time { get; set; }

        
    }
}
