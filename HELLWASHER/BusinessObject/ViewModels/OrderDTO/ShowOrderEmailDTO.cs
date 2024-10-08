using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class ShowOrderEmailDTO
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        
        public DateTime PaymentDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
