using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Domain;
using OnlineBanking.Data;
using OnlineBanking.Web.Models;

namespace OnlineBanking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly OnlineBankingDbContext dbContext;

        public HomeController(IHttpContextAccessor httpContextAccessor, OnlineBankingDbContext dbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = dbContext.Users.Include(u=>u.Account).FirstOrDefault(u => u.Id == Guid.Parse(userId));

            List<Transaction> transactions = new List<Transaction>();
            var debits = dbContext.Transactions.Where(t => t.SourceAccount == user.Account.Number).AsNoTracking().ToList();
            debits.ForEach(d => d.Type = TransactionType.Debit);
            var credits = dbContext.Transactions.Where(t => t.TargetAccount == user.Account.Number).AsNoTracking().ToList();
            transactions.AddRange(debits);
            transactions.AddRange(credits);

            return View(new HomePageViewModel() { Transactions = transactions, Balance = user.Account.Balance, LastTransaction = GetLastTransaction(transactions,user.Account.Number)
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        private LastTransaction GetLastTransaction(List<Transaction> transactions, string accountNumber)
        {
            var transaction =transactions.OrderByDescending(t => t.TransactionDate).FirstOrDefault();

            return new LastTransaction()
            {
                Amount = transaction.Amount,
                Type = transaction.SourceAccount == accountNumber ? TransactionType.Debit : TransactionType.Credit
            };
        }
    }
}
