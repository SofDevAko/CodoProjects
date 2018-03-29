using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace spookify.Models
{
    public class RegUser : BaseEntity
    {
        [Required]
        [MinLength(2, ErrorMessage = "You must enter a User Name with 2 or more characters")]
        public string Username {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        [EmailAddress]
        [CompareAttribute("Email", ErrorMessage = "Email and Confirm Email must match!")]
        public string ConfirmEmail {get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [DateValid(ErrorMessage="You must have born in the past!")]
        public DateTime DoB {get;set;}
        [Required]
        public string Gender {get;set;}
        
        public class DateValidAttribute : ValidationAttribute
        {
            public override bool IsValid(object userdate)
            {
                return userdate != null && (DateTime)userdate < DateTime.Now;
            }
        }
    }

    public class LoginUser : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    public class LogReg : BaseEntity
    {
        public RegUser reg { get; set; }
        public LoginUser log { get; set; }
    }

    
}
