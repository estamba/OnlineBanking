using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Domain
{
   public class UserAddModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public double IntialBalance { get; set; }
        public string AdminUserId { get; set; }


    }
}
