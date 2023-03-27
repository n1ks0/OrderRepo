using Order.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BL.Interfaces
{
    public interface IProviderService
    {
        IEnumerable<Provider> GetAll();
    }
}
