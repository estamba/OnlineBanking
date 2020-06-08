using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Web.Models
{
    public class AddUserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        
        public string Password { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public double IntialBalance { get; set; }


    }
}
