using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoffeeStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private CoffeeStoreDbContext context;
        public EFOrderRepository(CoffeeStoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
        .Include(o => o.Lines)
        .ThenInclude(l => l.Coffee);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Coffee));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
