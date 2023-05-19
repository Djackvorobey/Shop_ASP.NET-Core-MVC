using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;
using TestProjectMVC.Data;
using TestProjectMVC.Models;

namespace TestProjectMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(10);
                Options.Cookie.HttpOnly= true;
                Options.Cookie.IsEssential= true;
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //
          builder.Services.AddDefaultIdentity<Users>().AddEntityFrameworkStores<ApplicationDbContext>();
           
           //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
              //  .AddEntityFrameworkStores<ApplicationDbContext>();
            
           // builder.Services.AddControllersWithViews();

            //add google authentication
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            //})
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            //    {
            //        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

            //        options.ClientId = googleAuthNSection["ClientId"];
            //        options.ClientSecret = googleAuthNSection["ClientSecret"];
            //        options.CallbackPath = "/Category/RedirectUrl";

            //    });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapRazorPages();
            app.MapControllerRoute(
                
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


           

            app.Run();
        }
    }
}