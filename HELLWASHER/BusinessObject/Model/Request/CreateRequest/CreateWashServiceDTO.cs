using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Request.CreateRequest
{
    public class CreateWashServiceDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int ServiceCategoryId { get; set; }
        [Required]
        public string ClothUnit { get; set; }
        [Required]
        public decimal Price { get; set; }
        // Use IFormFile for local file uploads
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
}
