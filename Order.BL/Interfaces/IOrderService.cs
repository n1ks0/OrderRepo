namespace Order.BL.Interfaces
{
    public interface IOrderService
    {
        Task Create(BL.Entities.Order order);
        Task Delete(int? id);
        Task Edit(BL.Entities.Order order);
        IEnumerable<BL.Entities.Order> GetAll();
        Task<IEnumerable<BL.Entities.Order>> GetOrdersInPeriod(DateTime start, DateTime end);
        Task<BL.Entities.Order> GetOrderById(int id);
        Task<IEnumerable<Order.BL.Entities.Order>> GetFilteredOrders(DateTime start, DateTime end,
            IEnumerable<int> providerIds, IEnumerable<string> orderNumbers);
        bool IsOrderNumberOccupied(string? number, int providerId);
    }
}