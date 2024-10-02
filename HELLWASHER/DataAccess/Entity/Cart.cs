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
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation properties
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
