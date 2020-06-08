using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Interfaces
{
   public interface IAccountService
    {
        bool Login(string userName, string password);
    }
}
