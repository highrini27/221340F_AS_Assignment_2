using _221340F_AS_Assignment_2.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;


namespace _221340F_AS_Assignment_2.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager; // Use ApplicationUser

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (ModelState.IsValid)
            //{
            //    // Attempt to sign in the user
            //    var result = await _signInManager.PasswordSignInAsync(EmailAddress, Password, true, false);
            //    if (result.Succeeded)
            //    {
            //        // Sign in successful, redirect to home page
            //        return RedirectToPage("/Home"); // Or any desired destination
            //    }
            //    else
            //    {
            //        // Sign in failed, display an error message
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //    }
            //}

            if (ModelState.IsValid)
            {
                if (ValidateCaptcha())
                {
                    // Attempt to sign in the user
                    var result = await _signInManager.PasswordSignInAsync(EmailAddress, Password, true, false);
                    if (result.Succeeded)
                    {
                        // Sign in successful, redirect to home page
                        return RedirectToPage("/Home");
                    }
                    else
                    {
                        // Sign in failed, display an error message
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Validate captcha to prove you're human.");
                }
            }


            return Page();
        }
        public void OnGet()
        {
        }

        // MyObject class within Login.cshtml.cs:
        public class MyObject
        {
            public bool success { get; set; }
            // Add other properties if needed, based on the JSON response
        }

        public bool ValidateCaptcha()
        {
            bool result = false;

  

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(
                "https://www.google.com/recaptcha/api/siteverify?secret=6LdnMmQpAAAAACyofVV0yaQH13aqLKBv4A5ll8no");

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    // Using JavaScriptSerializer:
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
                    //result = Convert.ToBoolean(jsonObject.success);

                    // Or, using System.Text.Json:
                    MyObject jsonObject = JsonSerializer.Deserialize<MyObject>(jsonResponse);
                    result = jsonObject.success;
                }
            }

            catch (WebException ex)
            {
                throw ex;
            }

            return result;
            }
           
        }
    }