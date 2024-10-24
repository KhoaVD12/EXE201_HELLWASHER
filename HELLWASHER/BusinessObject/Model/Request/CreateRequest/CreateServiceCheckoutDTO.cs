using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.CreateRequest
{
    public class CreateServiceCheckoutDTO
    {
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public decimal Weight { get; set; }
        
    }
}
