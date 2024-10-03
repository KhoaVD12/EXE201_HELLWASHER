using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request
{
    public class CreateWashServiceDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int ClothUnitId { get; set; }
        public int ServiceTypeId { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
    }
}
