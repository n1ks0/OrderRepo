using System.ComponentModel.DataAnnotations;

namespace Order.Web.Models.Order
{
    public class OrderItemDto
    {
        [Required, Display(Name = "Наименование")]
        public string? Name { get; set; }

        [Required, Display(Name = "Количество")]
        public decimal Quantity { get; set; }

        [Required, Display(Name = "Единица измерения")]
        public string? Unit { get; set; }
    }
}
