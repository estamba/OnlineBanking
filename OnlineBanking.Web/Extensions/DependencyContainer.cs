using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Core.Interfaces;
using OnlineBanking.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Web.Extensions
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAdminService, AdminService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITransactionService, TransactionService>();




            return services;

        }
    }
}
