using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Response
{
    public class ResponseServiceCheckoutDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int QuantityPerService { get; set; }
        public decimal TotalPricePerService { get; set; }
    }
}
