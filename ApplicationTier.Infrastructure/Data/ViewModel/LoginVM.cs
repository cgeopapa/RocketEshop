using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.Infrastructure.Data.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Adress is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
