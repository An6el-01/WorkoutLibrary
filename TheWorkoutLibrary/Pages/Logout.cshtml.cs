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
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> sm)
        {
            _signInManager = sm;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

    }
}
