﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.CreateRequest
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}