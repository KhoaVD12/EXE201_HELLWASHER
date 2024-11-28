using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string Bin { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int OrderId {  get; set; }
        public long OrderCode { get; set; }
        public string Currency { get; set; }
        public string PaymentLinkId { get; set; }
        public string Status { get; set; }
        public long? ExpiredAt { get; set; }
        public string CheckoutUrl { get; set; }
        public string QrCode { get; set; }
    }
}
