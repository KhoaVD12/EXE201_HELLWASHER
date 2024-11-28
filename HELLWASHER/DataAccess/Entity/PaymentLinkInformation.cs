using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class PaymentLinkInformation
    {
        public int PaymentLinkInformationId { get; set; }
        public string Id { get; set; }
        public long OrderCode { get; set; }
        public int Amount { get; set; }
        public int AmountPaid { get; set; }
        public int AmountRemaining { get; set; }
        public string Status { get; set; }
        public string CreateAt { get; set; }
        public string? CancelAt { get; set; }
        public string? CancellationReason { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
