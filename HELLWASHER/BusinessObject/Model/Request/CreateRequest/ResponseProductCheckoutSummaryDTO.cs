using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.CreateRequest
{
    public class ResponseProductCheckoutSummaryDTO
    {
        public decimal TotalAmount { get; set; }
        public IEnumerable<ResponseProductCheckoutDTO> Services { get; set; }
    }
}
