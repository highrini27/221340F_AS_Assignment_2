using Microsoft.AspNetCore.Identity;

namespace _221340F_AS_Assignment_2.Model
{
	public class ApplicationUser: IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string Gender { get; set; }

		public string DOB { get; set; }

		public string NRIC { get; set; }

		public string EmailAddress { get; set; }

		public string WhoAmI { get; set; }

		public string Resume { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }
	}
}
