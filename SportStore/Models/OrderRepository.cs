using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class OrderRepository : IOrderRepository
    {
        private DataContext  context;
        public OrderRepository(DataContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(p => p.Product);

        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            context.Remove(order);
            context.SaveChanges();
        }

        public Order GetOrder(long key)
        {
            return context.Orders.Include(l => l.Lines).First(k => k.Id == key);
        }

        public void UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }
    }
}
