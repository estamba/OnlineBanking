using Microsoft.EntityFrameworkCore.Internal;
using OnlineBanking.Core.Interfaces;
using OnlineBanking.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OnlineBanking.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly OnlineBankingDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;

        public AccountService(OnlineBankingDbContext dbContext, IPasswordHasher passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }
        public bool Login(string userName, string password)
        {
            var hashedPassword = passwordHasher.Hash(password);
            return dbContext.Users.Any(u => u.UserName == userName && u.Password == hashedPassword);
        }
    }
}
