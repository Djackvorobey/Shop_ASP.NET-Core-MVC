using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestProjectMVC.Models;

namespace TestProjectMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       
        public DbSet<Category> Category{ get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ApplicationType> ApplicationType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Users>(b =>
            {
                b.Property(u => u.PhoneNumber).HasMaxLength(15);
            });
        }
    }
}
