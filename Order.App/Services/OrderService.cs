using Order.BL.Interfaces;
using Order.BL.Interfaces.Repo;

namespace Order.App.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Create(Order.BL.Entities.Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            await _unitOfWork.Order.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _unitOfWork.Order.Delete(id.Value);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Edit(Order.BL.Entities.Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            await _unitOfWork.Order.UpdateFullOrder(order);
        }

        public IEnumerable<Order.BL.Entities.Order> GetAll()
        {
            return _unitOfWork.Order.GetAll();
        }

        public async Task<IEnumerable<BL.Entities.Order>> GetFilteredOrders(DateTime start, DateTime end, IEnumerable<int> providerIds, IEnumerable<string> orderNumbers)
        {
            return await _unitOfWork.Order.GetFilteredOrders(start, end, providerIds, orderNumbers);
        }

        public Task<BL.Entities.Order> GetOrderById(int id)
        {
            return _unitOfWork.Order.GetFullOrder(id);
        }

        public async Task<IEnumerable<BL.Entities.Order>> GetOrdersInPeriod(DateTime start, DateTime end)
        {
            return await _unitOfWork.Order.GetOrdersInPeriod(start, end);
        }

        public bool IsOrderNumberOccupied(string? number, int providerId)
        {
            return _unitOfWork.Order.GetAll().Any(o => o.Number == number && o.ProviderId == providerId);
        }
    }
}
