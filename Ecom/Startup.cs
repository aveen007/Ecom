using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using AppDbContext.UOW;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Services;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace Ecom
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Ecommerce_DBContext>(options => options.UseSqlServer(Configuration.
                GetConnectionString("DefaultConnection")));

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Ecommerce_DBContext>();

            //services.AddDefaultIdentity<ApplicationUser>()..AddRoles<IdentityRole>().AddEntityFrameworkStores<Ecommerce_DBContext>();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Ecommerce_DBContext>();
            services.AddDbService(Configuration);

            services.AddSingleton<ISingletonRnd, SingletonRnd>();   
            services.AddTransient<ITransientRnd, TransientRnd>();
            services.AddScoped<IScopedRnd, ScopedRnd>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAutoMapper(typeof(Startup));
        }   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
