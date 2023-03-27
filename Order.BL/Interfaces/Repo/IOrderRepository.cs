using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BL.Interfaces.Repo
{
    public interface IOrderRepository : IRepository<Entities.Order>
    {
        Task UpdateFullOrder(Entities.Order order);
        Task<IEnumerable<Entities.Order>> GetOrdersInPeriod(DateTime start, DateTime end);
        Task<Entities.Order> GetFullOrder(int id);
        Task<IEnumerable<Entities.Order>> GetFilteredOrders(DateTime start, DateTime end, IEnumerable<int> providerIds, IEnumerable<string> orderNumbers);
    }
}
