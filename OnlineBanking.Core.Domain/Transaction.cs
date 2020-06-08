using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string SourceAccount { get; set; }
        public string TargetAccount { get; set; }

        public decimal Amount { get; set; }

        public TransctionStatus Status { get; set; }

        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }

    }
}
