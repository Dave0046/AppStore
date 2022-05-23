using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            DataContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "LG 55 tum",
                    Description = "An OLED TV med Web OS Operating System",
                    Category = "TV's",
                    Price = 15000 
                },
                new Product
                {
                    Name = "Asus VivoBook 15 tum",
                    Description = "Asus bärbar dator, core i7, 16GB Ram, 256 SSD",
                    Category = "Bärbara Dator",
                    Price = 10000
                },
                new Product
                {
                    Name = "Iphone 13 pro max",
                    Description = "6,7 tum, 120Hz, 256 GB",
                    Category = "Phones",
                    Price = 14000
                }
                );

               
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var user = new User()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    Street = "123",
                    City = "Stockholm",
                    PostalCode = "1234AA12a"
                };
                var user2 = new User()
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                    Street = "123",
                    City = "Stockholm",
                    PostalCode = "1234AA12a"
                };
                UserManager<User> _userManager = app.ApplicationServices
               .CreateScope().ServiceProvider.GetRequiredService<UserManager<User>>();

                var result = _userManager.CreateAsync(user, "123456");
                var result2 = _userManager.CreateAsync(user2, "123456");
                var role = new IdentityRole()
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                var role2 = new IdentityRole()
                {
                    Name = "user",
                    NormalizedName = "user"
                };

                RoleManager<IdentityRole> _roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var result3 = _roleManager.CreateAsync(role);
                var reslutr4 = _roleManager.CreateAsync(role2);
                _userManager.AddToRoleAsync(user, "admin");
                _userManager.AddToRoleAsync(user2, "user");
            }
        }
    }
}
