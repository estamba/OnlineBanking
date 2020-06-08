using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Domain
{
    public class Account
    {
        string accountPrefix = "159002";
        public Account()
        {
            
        }
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public string Number { get; set; }
        public AccountStatus status { get; set; }
        public void SetAcountNumber()
        {
            Number = accountPrefix + GenerateAccountNumber().ToString();

        }
        private int GenerateAccountNumber()
        {
            Random rand = new Random();
           return rand.Next(200, 2000);
        }
            

    }
}
