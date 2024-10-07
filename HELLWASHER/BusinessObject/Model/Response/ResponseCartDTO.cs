using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Response
{
    public class ResponseCartDTO
    {
        public int CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public string CartStatus { get; set; }
        public ICollection<ResponseCartItemDTO> Items { get; set; } = [];
    }
}
