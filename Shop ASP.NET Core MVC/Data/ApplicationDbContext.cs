using Microsoft.EntityFrameworkCore;
using TestProjectMVC.Models;

namespace TestProjectMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       
        public DbSet<Category> Category{ get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ApplicationType> ApplicationType { get; set; }


    }
}
