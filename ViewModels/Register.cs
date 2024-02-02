using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _221340F_AS_Assignment_2.ViewModels
{
    public class Register
    {

		[Required]
		[DataType(DataType.Text)]
		public string FirstName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Gender { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public string DOB { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string NRIC { get; set; }

		[Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string WhoAmI { get; set; }

		[Required]
		[DataType(DataType.Upload)]
		public string Resume { get; set; }

		[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]

        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
