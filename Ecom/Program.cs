using Microsoft.AspNetCore.Hosting;
using AppDbContext.Models;
using Ecom.Seeders;
using Microsoft.Extensions.Configuration;
using AppDbContext.UOW;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ecom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Program.SeedData(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static void SeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                var unitOfWork = service.GetRequiredService<IUnitOfWork>();

                //DBSeeder.SeedDeleteUser(unitOfWork, userManager, roleManager);
                DBSeeder.SeedUser(unitOfWork, userManager, roleManager);
            }
        }
    }
  
}
