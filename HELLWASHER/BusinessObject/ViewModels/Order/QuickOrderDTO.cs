using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Order
{
    public class QuickOrderDTO
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        //[Phone]
        public string CusomterPhone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
