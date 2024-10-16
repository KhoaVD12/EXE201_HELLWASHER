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
        public bool IsDeleted { get; set; }
        public ProductEnum ProductStatus { get; set; }
        public string? ImageURL { get; set; }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                // Change the ProductStatus based on the quantity
                if (_quantity <= 0)
                {
                    ProductStatus = ProductEnum.OutOfStock; // Assuming ProductEnum.OutOfStock is a valid enum value
                }
                else
                {
                    ProductStatus = ProductEnum.Available; // Assuming ProductEnum.InStock is a valid enum value
                }
            }
        }
        public int CategoryId { get; set; }
        public ICollection<ProductCheckout> ProductCheckouts { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public Category Category { get; set; }
    }
}
