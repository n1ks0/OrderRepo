using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BL.Interfaces.Repo
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Order { get; }
        IOrderItemRepository OrderItem { get; }
        IProviderRepository Provider { get; }
        Task<int> SaveChangesAsync();
    }
}
