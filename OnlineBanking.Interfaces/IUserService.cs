using OnlineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Interfaces
{
    public interface IUserService
    {
        Result<string> AddUser(UserAddModel userAddModel);
    }
}
