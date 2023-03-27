using Microsoft.EntityFrameworkCore;
using Order.BL.Entities;
using Order.BL.Exceptions;
using Order.BL.Interfaces.Repo;
using Order.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DAL.Repository
{
    public class OrderRepository : Repository<BL.Entities.Order>, IOrderRepository
    {
        public OrderRepository(Context orderContext) : base(orderContext)
        {
        }

        public async Task<IEnumerable<BL.Entities.Order>> GetFilteredOrders(DateTime start, DateTime end,
            IEnumerable<int> providerIds, IEnumerable<string> orderNumbers)
        {
            var orders = orderContext.Orders
                .Include(o => o.Provider)
                .Include(o => o.OrderItems)
                .Where(o => o.Date >= start &&
                            o.Date <= end);

            if (providerIds != null && providerIds.Any())
            {
                orders = orders.Where(o => providerIds.Contains(o.ProviderId));
            }
            if (orderNumbers != null && orderNumbers.Any())
            {
                orders = orders.Where(o => orderNumbers.Contains(o.Number));
            }

            return await orders.ToListAsync();       
        }

        public async Task<BL.Entities.Order> GetFullOrder(int id)
        {
            return await orderContext.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Provider)                
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<BL.Entities.Order>> GetOrdersInPeriod(DateTime start, DateTime end)
        {
           return await orderContext.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Provider)
                .Where(o => 
                    o.Date >= start &&
                    o.Date <= end)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateFullOrder(BL.Entities.Order order)
        {
            orderContext.Update(order);

            var toDelete = order.OrderItems?.Where(o => o.IsDeleted);

            if (toDelete != null && toDelete.Any())
            {
                orderContext.RemoveRange(toDelete);
            }
            
            await orderContext.SaveChangesAsync();
        }
    }
}
