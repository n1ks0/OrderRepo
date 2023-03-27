using System.ComponentModel.DataAnnotations;

namespace Order.Web.Models.Order
{
    public class OrderCreateViewModel
    {
        [Required, Display(Name = "Номер заказа")]
        public string? Number { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public List<OrderItemCreateDto>? OrderItems { get; set; }
    }

    public class OrderItemCreateDto : OrderItemDto
    {

    }
}
