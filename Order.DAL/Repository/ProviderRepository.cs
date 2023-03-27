using Order.BL.Entities;
using Order.BL.Interfaces.Repo;
using Order.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DAL.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(Context orderContext) : base(orderContext)
        {
        }
    }
}
