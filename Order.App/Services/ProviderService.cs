using Order.BL.Entities;
using Order.BL.Interfaces;
using Order.BL.Interfaces.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.App.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IEnumerable<Provider> GetAll()
        {
            return _unitOfWork.Provider.GetAll();
        }
    }
}
