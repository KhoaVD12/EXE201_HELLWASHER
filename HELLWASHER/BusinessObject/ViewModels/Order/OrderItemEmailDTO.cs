using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class OrderItemEmailDTO
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Address { get; set; }
        public DateTime? PickUpDate { get; set; }

    }
}
