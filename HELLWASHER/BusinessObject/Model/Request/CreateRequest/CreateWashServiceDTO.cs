using Microsoft.AspNetCore.Http;
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
        
        public string ClothUnit { get; set; }
        public decimal Price { get; set; }
        // Use IFormFile for local file uploads
        public IFormFile? ImageFile { get; set; }

        // Use this for image URL uploads
        public string? ImageURL { get; set; }
    }
}
