using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
		[Required(ErrorMessage = "Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Passward  Length is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }  //**********

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare(/*"Password"*/nameof(NewPassword), ErrorMessage = "Confirm New Password does Not Matche New Password")]
		[DataType(DataType.Password)]
		public string ConfirmNewPassword { get; set; }


    }
}
