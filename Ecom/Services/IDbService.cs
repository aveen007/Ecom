using AppDbContext.Models;
using AppDbContext.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FirstWebApp.Services
{
    public static class IDbService
    {
        public static IServiceCollection AddDbService(this IServiceCollection services,
            IConfiguration Configuration)
        {
            services.AddDbContext<Ecommerce_DBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
