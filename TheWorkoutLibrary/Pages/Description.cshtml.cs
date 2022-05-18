using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWorkoutLibrary.Data;
using Microsoft.AspNetCore.Identity;

namespace TheWorkoutLibrary.Pages
{
    public class DescriptionModel : PageModel
    {
        [BindProperty]
        public Workout workout { get; set; }

        public IList<WorkoutExcercise> workoutExcercises { get; set; }
        
        public IList<Excercise> excercises { get; private set; }

        private readonly AppDbContext _db;

        private readonly UserManager<ApplicationUser> _um;

        public DescriptionModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            excercises = _db.Excercise.ToList();
            workoutExcercises = _db.WorkoutExcercise.ToList();
            workout = await _db.Workout.FindAsync(id);           

            if (workout == null)
            {
                return RedirectToPage("/Index");
            }  
            return Page();
        }
    }
}
