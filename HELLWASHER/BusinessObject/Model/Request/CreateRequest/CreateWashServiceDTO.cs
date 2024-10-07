using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.CreateRequest
{
    public class CreateWashServiceDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string ClothUnit { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
    }
}
