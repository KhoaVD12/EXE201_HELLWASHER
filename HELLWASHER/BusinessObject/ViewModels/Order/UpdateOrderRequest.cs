using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class UpdateOrderRequest
    {
        public int PaymentMethodId { get; set; }
        public string Address { get; set; }
        public DateTime? PickUpDate { get; set; }
        public string? ConfirmImage { get; set; }

    }
}
