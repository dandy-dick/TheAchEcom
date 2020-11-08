using System;
using System.Collections.Generic;
using Repository.DomainModels;
using Repository.BusinessModels;
using Repository.BusinessModels.ShopList;
using System.ComponentModel.DataAnnotations;

namespace TheAchEcom.Models
{
    public class AccountModel
    {
        [Required]
        [Display(Name="Your Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType("Password")]
        public string Password { get; set; }

        [Required]
        [DataType("Password")]
        [Display(Name = "Password Confirmation")]
        [Compare("Password", ErrorMessage = "Password & Password Confirmation is not matched!!")]
        public string ConfirmPassword { get; set; }

        [Display(Name="Save password to browser")]
        public bool RememberMe { get; set; } = false;
    }
}
