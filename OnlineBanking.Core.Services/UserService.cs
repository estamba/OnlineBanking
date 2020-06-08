using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Domain;
using OnlineBanking.Core.Interfaces;
using OnlineBanking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineBanking.Core.Services
{
    public class UserService : IUserService
    {
        private readonly OnlineBankingDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;

        public UserService(OnlineBankingDbContext dbContext, IPasswordHasher passwordHasher )
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }
        public Result<string> AddUser(UserAddModel userAddModel)
        {
            User user = dbContext.Users.FirstOrDefault(u => u.UserName == userAddModel.UserName);
            if (user != null) return new Result<string>() { IsSuccessFul = false, Data = "User name already exist" };

           user = new User()
            {
                Id = Guid.NewGuid(),
                Name = userAddModel.Name,
                Password = passwordHasher.Hash(userAddModel.Password),
                Role = UserRole.Standard,
                UserName = userAddModel.UserName
                

            };
            Account account = new Account()
            {
                Balance = (decimal)userAddModel.IntialBalance,
                Id = Guid.NewGuid(),
                UserId = user.Id,

            };
            
            account.SetAcountNumber();

            user.Account = account;

            dbContext.Users.Add(user);
            var admin = dbContext.Users.Include(u => u.Account).FirstOrDefault(a => a.Id == Guid.Parse(userAddModel.AdminUserId));
            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Description = "Initial account setup",
                Amount = (decimal)userAddModel.IntialBalance,
                SourceAccount = admin.Account.Number,
                TargetAccount = account.Number,
                Status = TransctionStatus.Pending,
                TransactionDate = DateTime.Now,
                Type = TransactionType.Credit
            };
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
            return new Result<string>() { IsSuccessFul = true };


        }
    }
}
