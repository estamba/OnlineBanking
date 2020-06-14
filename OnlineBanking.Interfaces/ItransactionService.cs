using OnlineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Interfaces
{
   public interface ITransactionService
    {
        Result<string> AddTransaction(AddTransferModel addTransferModel);
        Result<string> CompleteTransaction(string transactionId);

    }
}
