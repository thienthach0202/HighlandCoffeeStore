using System.Linq;
namespace CoffeeStore.Models
{
    public class EFCoffeeStoreRepository : ICoffeeStoreRepository
    {
        private CoffeeStoreDbContext context;
        public EFCoffeeStoreRepository(CoffeeStoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Coffee> Coffees => context.Coffees;
        public void CreateCoffee(Coffee b)
        {
            context.Add(b);
            context.SaveChanges();
        }
        public void DeleteCoffee(Coffee b)
        {
            context.Remove(b); context.SaveChanges();
        }
        public void SaveCoffee(Coffee b)
        {
            context.SaveChanges();
        }
    }
}