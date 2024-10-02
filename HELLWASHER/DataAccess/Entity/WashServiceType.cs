using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class WashServiceType
    {
        public int WashServiceTypeId { get; set; }
        public string? Name { get; set; }
        public ICollection<WashService>? Services { get; set; }
    }
}
