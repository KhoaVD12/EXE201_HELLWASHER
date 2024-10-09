using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ProductEnum ProductStatus { get; set; }
        public string? ImageURL { get; set; }

        public ICollection<ProductCheckout> ProductCheckouts { get; set; }
        public Category Category { get; set; }
    }
}
