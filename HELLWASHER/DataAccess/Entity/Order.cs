using DataAccess.Enum;
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
        public int CartId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Address { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? PickUpDate { get; set; }
        public OrderEnum OrderStatus { get; set; }
        public WashEnum WashStatus { get; set; }
        public Cart Cart { get; set; }
    }
}
