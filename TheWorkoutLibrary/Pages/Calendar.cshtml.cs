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
    public class CalendarModel : PageModel
    {

        [BindProperty] public Workout calendarWorkout { get; set; }

        public IList<WorkoutExcercise> workoutExcercisesCalendar { get; set; }

        public IList<Excercise> excercisesCalendar { get; private set; }

        private readonly AppDbContext _db;

        private readonly UserManager<ApplicationUser> _um;

        public CalendarModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            excercisesCalendar = _db.Excercise.ToList();
            workoutExcercisesCalendar = _db.WorkoutExcercise.ToList();
            calendarWorkout = await _db.Workout.FindAsync(1);

            if (calendarWorkout == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }


    }


}
