using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ServiceCategory
    {
        public int ServiceCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        // Navigation properties
        public ICollection<Service>? Services { get; set; }
    }
}
