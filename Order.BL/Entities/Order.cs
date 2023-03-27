using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
