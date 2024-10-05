using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request
{
    public class CreateCartItemDTO
    {
        public int CartId { get; set; }
        public int ServiceId { get; set; }
        public int QuantityPerSerivce { get; set; }
    }
}
