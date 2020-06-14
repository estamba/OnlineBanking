using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Core.Domain;
using OnlineBanking.Core.Interfaces;
using OnlineBanking.Data;
using OnlineBanking.Web.Models;

namespace OnlineBanking.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly OnlineBankingDbContext onlineBankingDbContext;

        public TransactionController(ITransactionService transactionService, OnlineBankingDbContext onlineBankingDbContext)
        {
            this.transactionService = transactionService;
            this.onlineBankingDbContext = onlineBankingDbContext;
        }
        public IActionResult Transfer()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Transfer(TransferViewModel transferViewModel)
        {
            if (!ModelState.IsValid) return View(transferViewModel);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var account = onlineBankingDbContext.Accounts.FirstOrDefault(u => u.UserId == Guid.Parse(userId));
            AddTransferModel addTransferModel = new AddTransferModel()
            {
                Amount = (decimal)transferViewModel.Amount,
                Description = transferViewModel.Description,
                SourceAccountNumber = account.Number,
                TargetAccountNumber = transferViewModel.BeneficiaryAccountNumber
            };
           var result = transactionService.AddTransaction(addTransferModel);
            return RedirectToAction("Confirm", new { transactionId = result.Data });

        }
        [HttpGet]
        public IActionResult Confirm(string transactionId)
        {


            return View( new ConfirmationViewModel() {TransactionId = transactionId });
        }
        [HttpPost]
        public IActionResult Confirm(ConfirmationViewModel confirmationViewModel)
        {
            if (!ModelState.IsValid) return View(confirmationViewModel);

            transactionService.CompleteTransaction(confirmationViewModel.TransactionId);
            return RedirectToAction("Index", "Home");
        }
    }
}