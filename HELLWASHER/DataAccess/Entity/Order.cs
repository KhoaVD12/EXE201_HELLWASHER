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
        public int? UserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CusomterPhone { get; set; }
        //public int? ServiceId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? PickUpDate { get; set; }
        public OrderEnum OrderStatus { get; set; }
        public WashEnum WashStatus { get; set; }
        public string? ConfirmImage {  get; set; }
        public User User { get; set; }
        public ICollection<ServiceCheckout>? ServiceCheckouts { get; set; }
        public ICollection<ProductCheckout>? ProductCheckouts { get; set; }
    }
}
