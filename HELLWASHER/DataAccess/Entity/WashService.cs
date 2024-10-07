﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class WashService
    {
        public int WashServiceId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string ClothUnit { get; set; }
        public decimal Price { get; set; }
        public int ServiceStatusId { get; set; }
        public string? ImageURL { get; set; }
        // Navigation properties
        public Category Category { get; set; }
        public WashServiceStatus ServiceStatus { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
