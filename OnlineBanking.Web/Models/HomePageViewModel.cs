using OnlineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Web.Models
{
    public class HomePageViewModel
    {
        public List<Transaction> Transactions { get; set; }
    }
}
