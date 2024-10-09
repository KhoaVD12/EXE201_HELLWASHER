﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ProductCheckout
    {
        public int ProductCheckoutId { get; set; }
        public int ProductId { get; set; }
        public int QuantityPerService { get; set; }
        public decimal TotalPricePerService { get; set; }
        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
