using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        
        public UserProfile userProfile = new UserProfile();
        public Workout workout = new Workout();

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


                    var currentUser = _db.userProfiles.FromSqlRaw("SELECT * FROM UserProfiles")
                        .OrderByDescending(b => b.UserId)
                        .FirstOrDefault();
                    if (currentUser == null)
                    {
                        userProfile.UserId = 1;
                    }
                    else
                    {
                        userProfile.UserId = currentUser.UserId + 1;                        
                    }
                    userProfile.Email = Input.Email;                   
                    userProfile.FirstName = Convert.ToString(Request.Form["firstName"]);
                    userProfile.LastName = Convert.ToString(Request.Form["lastName"]);
                    userProfile.StartDate = Convert.ToDateTime(Request.Form["startDate"]);
                    userProfile.SubscriptionStatus = true;
                    foreach (var file in Request.Form.Files)
                    {
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        userProfile.ImageData = ms.ToArray();
                        ms.Close();
                        ms.Dispose();
                    }
                    _db.userProfiles.Add(userProfile);
                    await _db.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
