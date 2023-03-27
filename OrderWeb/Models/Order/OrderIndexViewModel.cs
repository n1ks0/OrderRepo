using Order.BL.Entities;

namespace Order.Web.Models.Order
{
    public class OrderIndexViewModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<OrderIndexDto> Orders { get; set; }
    }

    public class OrderIndexDto
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime Date { get; set; }
        public string Provider { get; set; }
        public decimal Quantity { get; set; }
    }
}
