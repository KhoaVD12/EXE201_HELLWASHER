using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Address { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public int WashStatusId { get; set; }
        public DateTime? PickUpDate { get; set; }
        public User User { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public WashingStatus WashStatus { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
