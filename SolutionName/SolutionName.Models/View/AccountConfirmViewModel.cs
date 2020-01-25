using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.Models.View
{
    public class AccountConfirmViewModel
    {
        [Required]
        [DisplayName("Confirmation Code")]
        public string ConfirmationCode { get; set; }
    }
}
