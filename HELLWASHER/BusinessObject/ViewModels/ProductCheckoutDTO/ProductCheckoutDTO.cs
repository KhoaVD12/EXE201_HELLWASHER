using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.ProductCheckoutDTO
{
    public class ProductCheckoutDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int QuantityPerService { get; set; }
        [Required]
        public int orderId { get; set; }

    }
}
