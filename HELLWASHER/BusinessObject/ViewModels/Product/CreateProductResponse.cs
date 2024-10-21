using BusinessObject.Model.Request.CreateRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Product
{
    public class CreateProductResponse
    {
        public int productId { get; set; }
        public CreateProductDTO Product { get; set; }
        public string ImageUrl { get; set; }
    }
}
