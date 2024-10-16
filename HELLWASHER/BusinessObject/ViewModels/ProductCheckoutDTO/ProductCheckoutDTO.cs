using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.ProductCheckoutDTO
{
    public class ProductCheckoutDTO
    {
        public int ProductId { get; set; }
        public int QuantityPerService { get; set; }
        public int orderId { get; set; }

    }
}
