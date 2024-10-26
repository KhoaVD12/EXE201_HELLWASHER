using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.ProductCheckoutDTO
{
    public class ProductCheckoutResponse
    {
        public int Id { get; set; }
        public ProductCheckoutDTO ProductCheckout { get; set; }
    }
}
