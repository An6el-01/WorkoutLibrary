using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWorkoutLibrary.Data;

namespace TheWorkoutLibrary.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegistrationModel Input { get; set; }
        private AppDbContext _db;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userInManager;

        public RegisterModel(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm, AppDbContext db )
        {
            _signInManager = sm;
            _userInManager = um;
            _db = db;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userInManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userInManager.AddToRoleAsync(user, "Member");
                    return RedirectToPage("/Index");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
