using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDTO
{
    public class ShowOrderEmailDTO
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalProduct { get; set; }
        public decimal TotalService { get; set; }
        public User User { get; set; }
        public ICollection<ServiceCheckout>? ServiceCheckouts { get; set; }
        public ICollection<ProductCheckout>? ProductCheckouts { get; set; }
    }
}
