using OnlineBanking.Core.Domain;
using OnlineBanking.Core.Interfaces;
using OnlineBanking.Data;
using System;

namespace OnlineBanking.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly OnlineBankingDbContext dbContext;

        public AdminService(IPasswordHasher passwordHasher, OnlineBankingDbContext dbContext)
        {
            this.passwordHasher = passwordHasher;
            this.dbContext = dbContext;
        }
        public void AddAdmin(AdminAddModel adminAddModel)
        {

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = adminAddModel.Name,
                Password = passwordHasher.Hash(adminAddModel.Password),
                Role = UserRole.Admin,
                UserName = adminAddModel.UserName

            };
           Account account = new Account()
           {
               Balance = 0,
               Id = Guid.NewGuid(),
               UserId = user.Id,


           };
            account.SetAcountNumber();

            user.Account = account;

            dbContext.Users.Add(user);


            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Description = "Initial account setup",
                Amount = 0,
                SourceAccount = account.Number,
                TargetAccount = account.Number,
                Status = TransctionStatus.Successful,
                TransactionDate = DateTime.Now,
                Type = TransactionType.Credit
                 
                
            };
            dbContext.Transactions.Add(transaction);

            dbContext.SaveChanges();

          
        }
    }
}
