using OnlineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Web.Models
{
    public class UserListViewModel
    {
        public List<UserModel> Users { get; set; }
    }
    public class UserModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
    }
}
