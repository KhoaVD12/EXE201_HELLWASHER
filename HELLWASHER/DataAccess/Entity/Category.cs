using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? Status { get; set; }
        // Navigation properties
        public ICollection<WashService>? Services { get; set; }
    }
}
