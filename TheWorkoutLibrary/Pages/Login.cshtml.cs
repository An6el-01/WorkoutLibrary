using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using TheWorkoutLibrary.Data;
namespace TheWorkoutLibrary.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginUser Input { get; set; }

        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager) 
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                }
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
