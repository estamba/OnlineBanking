using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Web.Models
{
    public class TransferViewModel
    {
        [Required (ErrorMessage ="account number is required")]
        public string BeneficiaryAccountNumber { get; set; }
        [Required(ErrorMessage = "beneficiary name is required")]
        public string BeneficiaryName { get; set; }
        [Required(ErrorMessage = "beneficiary bank is required")]

        public string BeneficiaryBank{ get; set; }

        [Required(ErrorMessage = "beneficiary bank swift is required")]

        public string BeneficiaryBankSwiftCode { get; set; }
        [Required(ErrorMessage = "purpose of transfer is required")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Amount is required")]

        public double Amount { get; set; }




    }
}
