using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.Models.View
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("E-mail Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Reset Token")]
        public string ResetToken { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
