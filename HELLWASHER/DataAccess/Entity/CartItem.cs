using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ServiceId { get; set; }
        public int QuantityPerService { get; set; }
        public decimal TotalPricePerService { get; set; }
        // Navigation properties
        public Cart Cart { get; set; }
        public WashService Service { get; set; }
    }

}
