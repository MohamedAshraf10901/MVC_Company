using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name Is Reqeurd")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Reqeurd")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name Is Reqeurd")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "Minimum Passward  Length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }  //**********

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(/*"Password"*/nameof(Password), ErrorMessage = "Confirm Password does Not Matche Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }


      //  public IFormFile Image { get; set; }

    }
}
