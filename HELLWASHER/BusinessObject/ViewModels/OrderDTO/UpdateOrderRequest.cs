using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class UpdateOrderRequest
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Address { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public int WashStatusId { get; set; }
        public DateTime PickUpDate { get; set; }
    }
}
