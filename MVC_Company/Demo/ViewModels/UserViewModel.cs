using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Demo.PL.ViewModels
{
	public class UserViewModel
	{

		public string Id { get; set; }
		//public IFormFile Image { get; set; }
		public string ImageUrl { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }

		public IList<string> Roles { get; set; }


	}
}
