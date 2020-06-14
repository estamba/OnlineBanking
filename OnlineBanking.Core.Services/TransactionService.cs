using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Domain;
using OnlineBanking.Core.Interfaces;
using OnlineBanking.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly OnlineBankingDbContext dbContext;

        public TransactionService(OnlineBankingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Result<string> AddTransaction(AddTransferModel addTransferModel)
        {
            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Amount = addTransferModel.Amount,
                Description = addTransferModel.Description,
                SourceAccount = addTransferModel.SourceAccountNumber,
                TargetAccount = addTransferModel.TargetAccountNumber,
                Type = TransactionType.Debit,
                Status = TransctionStatus.Pending,
                TransactionDate = DateTime.Now
            };
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
            return new Result<string>() { IsSuccessFul = true, Data = transaction.Id.ToString() };
        }

        public Result<string> CompleteTransaction(string transactionId)
        {
          var transaction =  dbContext.Transactions.Find(Guid.Parse(transactionId));

            transaction.Status = TransctionStatus.Successful;
            dbContext.SaveChanges();

            return new Result<string>() { IsSuccessFul = true };
        }
    }
}
