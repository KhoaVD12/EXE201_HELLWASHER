﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.CreateRequest
{
    public class CreateCartItemDTO
    {
        public int CartId { get; set; }
        public int ServiceId { get; set; }
        public int QuantityPerService { get; set; }
        
    }
}