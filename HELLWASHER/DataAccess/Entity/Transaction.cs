using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int PaymentLinkInformationId { get; set; }
        public string Reference { get; set; }
        public int Amount { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string TransactionDateTime { get; set; }
        public string? VirtualAccountName { get; set; }
        public string? VirtualAccountNumber { get; set; }
        public string? CounterAccountBankId { get; set; }
        public string? CounterAccountBankName { get; set; }
        public string? CounterAccountName { get; set; }
        public string? CounterAccountNumber { get;set; }

        public PaymentLinkInformation PaymentLinkInformation { get; set; }
    }
}
