using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ServiceCheckout
    {
        public int ServiceCheckoutId { get; set; }
        public int ServiceId { get; set; }
        public int QuantityPerService { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPricePerService { get; set; }
        // Navigation properties
        public Order Order { get; set; }
        public Service Service { get; set; }
    }

}
