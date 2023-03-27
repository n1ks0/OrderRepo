using Order.BL.Interfaces.Repo;
using Order.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Order = new OrderRepository(_context);
            OrderItem= new OrderItemRepository(_context);
            Provider= new ProviderRepository(_context);
        }

        public IOrderRepository Order { get; private set; }

        public IOrderItemRepository OrderItem { get; private set; }

        public IProviderRepository Provider { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
