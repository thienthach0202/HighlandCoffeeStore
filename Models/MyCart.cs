using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class MyCart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
    public virtual void AddItem(Coffee coffee, int quantity)
    {
        CartLine line = Lines
        .Where(b => b.Coffee.CoffeeID == coffee.CoffeeID)
        .FirstOrDefault();
        if (line == null)
        {
            Lines.Add(new CartLine
            {
                Coffee = coffee,
                Quantity = quantity
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }
        public virtual void RemoveLine(Coffee coffee) =>
        Lines.RemoveAll(l => l.Coffee.CoffeeID == coffee.CoffeeID);
         public decimal ComputeTotalValue() =>
        Lines.Sum(e => e.Coffee.Price * e.Quantity);
        public virtual void Clear() => Lines.Clear();
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Coffee Coffee { get; set; }
        public int Quantity { get; set; }
    }
}


