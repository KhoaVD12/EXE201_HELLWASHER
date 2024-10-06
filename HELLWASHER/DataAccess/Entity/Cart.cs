using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Cart
    {
        public int CartId { get; set; }
        // Foreign Key
        public decimal TotalPrice { get; set; }
        public string CartStatus { get; set; }

        // Navigation properties
        public ICollection<CartItem>? CartItems { get; set; }
        public Order Order { get; set; }
    }
}
