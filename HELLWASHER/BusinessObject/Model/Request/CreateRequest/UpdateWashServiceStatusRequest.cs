using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.UpdateRequest.Entity
{
    public class UpdateWashServiceStatusRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
