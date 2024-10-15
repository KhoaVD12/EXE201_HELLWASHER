using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class OrderStatusRequest
    {
        public int OrderId { get; set; }
        public OrderEnum Status { get; set; }
    }
}
