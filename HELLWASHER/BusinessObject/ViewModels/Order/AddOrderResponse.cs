using BusinessObject.ViewModels.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Order
{
    public class AddOrderResponse
    {
        public int OrderId { get; set; }
        public QuickOrderDTO Order { get; set; }
    }
}
