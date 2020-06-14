using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Web.Models
{
    public class ConfirmationViewModel
    {
        public string TransactionId { get; set; }

        [Required]
        public string Code { get; set; }

    }
}
