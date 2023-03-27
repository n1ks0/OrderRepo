using System.ComponentModel.DataAnnotations.Schema;

namespace Order.BL.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; }
    }
}