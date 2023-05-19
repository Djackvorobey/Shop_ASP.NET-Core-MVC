using Microsoft.AspNetCore.Identity;

namespace TestProjectMVC.Models
{
    public class Users: IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime time { get; set; }
    }
}
