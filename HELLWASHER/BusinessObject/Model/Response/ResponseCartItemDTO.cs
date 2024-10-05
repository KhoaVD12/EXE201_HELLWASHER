using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Response
{
    public class ResponseCartItemDTO
    {
        public int CartId { get; set; }
        public int ServiceId { get; set; }
        public int QuantityPerSerivce { get; set; }
    }
}
