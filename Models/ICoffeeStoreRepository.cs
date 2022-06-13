using System.Linq;
namespace CoffeeStore.Models
{
    public interface ICoffeeStoreRepository
    {
        IQueryable<Coffee> Coffees { get; }
        void SaveCoffee(Coffee b);
        void CreateCoffee(Coffee b);
        void DeleteCoffee(Coffee b);
    }
}