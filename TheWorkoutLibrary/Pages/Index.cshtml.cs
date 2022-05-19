using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWorkoutLibrary.Data;


namespace TheWorkoutLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IList<Workout> workouts { get; private set; }
        public IList<Workout> recentWorkouts { get; private set; }
        public IList<Workout> oldestWorkouts { get; private set; }

        [BindProperty] public LoginUser Input { get; set; }

        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public IndexModel(AppDbContext db, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _signInManager = signInManager;
        }


        public void OnGet()
        {
            workouts = _db.Workout.ToList();
            recentWorkouts = workouts.Take(3).ToList();
            workouts = workouts.Reverse().ToList();
            oldestWorkouts = workouts.Take(3).ToList();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false,
                        lockoutOnFailure: false);
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
    }
}

