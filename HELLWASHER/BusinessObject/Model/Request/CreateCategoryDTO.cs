﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
    }
}