using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class OrderDTO
    {
        public int CartId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Address { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public int WashStatusId { get; set; }
        public DateTime? PickUpDate { get; set; }
    }
}
