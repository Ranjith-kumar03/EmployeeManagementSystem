using EmployeeManagementSystem.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse", controller:"Account")]
        //Create Class  validEmailDomainAttribute And extend abstarct class :ValidationAttribute
        //for allowedDomain:"gmail.com" create a constructor in the class validEmailDomainAttribute
        // over ride the below method which is commented
        //public override bool IsValid(object value)
        //{
        //    string[] strings = value.ToString().Split('@');
        //    return strings[1].ToUpper() == allowedDomain.ToUpper();
        
        [validEmailDomainAttribute(allowedDomain:"gmail.com",
            ErrorMessage ="Email Doimain must be Gmail.com")]// if the domain name is not ["gmail.com"] we need to create error message
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]//For masking Characters
        public string Password { get; set; }
       
        [DataType(DataType.Password)]//For masking Characters
        [Display(Name="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirmation Password do not Match.")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
