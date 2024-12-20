﻿using System;
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
        [Range(1, 20, ErrorMessage = "Quantity must be greater than 0")]
        public int QuantityPerProduct { get; set; }

    }
}
