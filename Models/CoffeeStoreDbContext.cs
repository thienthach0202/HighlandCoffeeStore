using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class CoffeeStoreDbContext : DbContext
    {
        public CoffeeStoreDbContext(DbContextOptions<CoffeeStoreDbContext>
       options)
        : base(options) { }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
