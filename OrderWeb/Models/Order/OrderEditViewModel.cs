namespace Order.Web.Models.Order
{
    public class OrderEditViewModel
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int ProviderId { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItemEditDto>? OrderItems { get; set; }
    }

    public class OrderItemEditDto : OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
