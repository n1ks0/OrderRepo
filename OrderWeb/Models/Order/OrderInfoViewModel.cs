namespace Order.Web.Models.Order
{
    public class OrderInfoViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Provider { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
