using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class UpdateOrderRequest
    {
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        public DateTime? PickUpDate { get; set; }
    }
}
