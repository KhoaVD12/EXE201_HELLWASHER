using BusinessObject.Model.Request.UpdateRequest.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Product
{
    public class UpdateProductResponse
    {
        public int productId { get; set; }
        public UpdateProductDTO Product { get; set; }
    }
}
