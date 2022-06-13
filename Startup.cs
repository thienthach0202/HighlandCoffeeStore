using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoffeeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoffeeStore
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
            services.AddControllersWithViews();
            services.AddDbContext<CoffeeStoreDbContext>(opts => {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:CoffeeStoreConnection"]);
            });
            services.AddScoped<ICoffeeStoreRepository, EFCoffeeStoreRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<MyCart>(sp => MySessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddServerSideBlazor();
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

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("genpage",
                "{genre}/{coffeePage:int}",
                new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page", "{coffeePage:int}",
                new { Controller = "Home", action = "Index", coffeePage = 1 });
                endpoints.MapControllerRoute("genre", "{genre}",
                new { Controller = "Home", action = "Index", coffeePage = 1 });
                endpoints.MapControllerRoute("pagination",
                "Coffees/{coffeePage}",
                new { Controller = "Home", action = "Index", coffeePage = 1 });
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}",
                "/Admin/Index");
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
