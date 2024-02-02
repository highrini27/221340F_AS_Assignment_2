using _221340F_AS_Assignment_2.Model;
using _221340F_AS_Assignment_2.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace _221340F_AS_Assignment_2.Pages
{
	public class RegisterModel : PageModel
	{
		public static bool ValidatePasswordStrength(string password)
		{
			// Minimum length of 12 characters
			if (password.Length < 12)
			{
				return false;
			}

			// Regex to check for required character types
			var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{12,}$");
			return regex.IsMatch(password);
		}

		private UserManager<ApplicationUser> userManager { get; }
		private SignInManager<ApplicationUser> signInManager { get; }

		[BindProperty]
		public Register RModel { get; set; }

		public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public void OnGet()
		{
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
				var protector = dataProtectionProvider.CreateProtector("MySecretKey");

				if (!ValidatePasswordStrength(RModel.Password))
				{
					if (RModel.Password.Length < 12)
					{
						ModelState.AddModelError("Password", "Password must be at least 12 characters long.");
					}
					else
					{
						ModelState.AddModelError("Password", "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.");
					}
					//ModelState.AddModelError("Password", "Password does not meet complexity requirements.");
					return Page();
				}

				var user = new ApplicationUser()
				{
					UserName = RModel.EmailAddress,
					FirstName = RModel.FirstName,
					LastName = RModel.LastName,
					Gender = RModel.Gender,
					DOB = RModel.DOB,
					NRIC = protector.Protect(RModel.NRIC),
					EmailAddress = RModel.EmailAddress,
					WhoAmI = RModel.WhoAmI,
					Resume = RModel.Resume,
					Password = RModel.Password,
					ConfirmPassword = RModel.ConfirmPassword,
					

				};
				var result = await userManager.CreateAsync(user, RModel.Password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return RedirectToPage("Index");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}


			}

			return Page();
		}
	}
}
