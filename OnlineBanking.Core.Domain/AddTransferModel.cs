using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Domain
{
    public class AddTransferModel
    {
        public string SourceAccountNumber { get; set; }
        public string TargetAccountNumber { get; set; }

        public decimal Amount { get; set; }
        public string  Description { get; set; }



    }
}
