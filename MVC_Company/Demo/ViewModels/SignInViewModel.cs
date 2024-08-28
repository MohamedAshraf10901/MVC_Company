using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Passward  Length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
