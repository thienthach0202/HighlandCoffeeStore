using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace CoffeeStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            CoffeeStoreDbContext context =
           app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService <CoffeeStoreDbContext> ();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Coffees.Any())
            {
                context.Coffees.AddRange(
                new Coffee
                {
                    Title = "Cà phê Chồn",
                   
                    Genre = "Coffee",
                    Price = 20m
                },
                new Coffee
                {
                    Title = "Cà phê dát vàng",
                   
                    Genre = "Coffee",
                    Price = 12m
                },
                new Coffee
                {
                    Title = "Trà Sen Vàng",
                    
                    Genre = "Trà",
                    Price = 5m
                },
                new Coffee
                {
                    Title = "Trà Đào ",
                    
                    Genre = "Trà",
                    Price = 3m
                },
                new Coffee
                {
                    Title = "Bánh Phô mai Trà Xanh",
                    
                    Genre = "Bánh",
                    Price = 10m
                }
                );
                context.SaveChanges();
            }
        }
    }
}