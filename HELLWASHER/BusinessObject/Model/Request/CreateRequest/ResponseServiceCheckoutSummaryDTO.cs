using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.CreateRequest
{
    public class ResponseServiceCheckoutSummaryDTO
    {
        public decimal TotalAmount { get; set; }
        public IEnumerable<ResponseServiceCheckoutDTO> Services { get; set; }
    }

}
