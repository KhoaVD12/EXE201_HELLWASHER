using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.UpdateRequest.Entity
{
    public class UpdateCartDTO
    {
        public decimal TotalPrice { get; set; }
        public string CartStatus { get; set; }
    }
}
