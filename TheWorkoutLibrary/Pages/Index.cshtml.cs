using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWorkoutLibrary.Data;

namespace TheWorkoutLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IList<Workout> workouts { get; private set; }
        public IList<Workout> recentWorkouts { get; private set; }

        public IndexModel(AppDbContext db)
        {
            _db = db;
           
        }
        public void OnGet()
        {
            workouts = _db.Workout.ToList();
            recentWorkouts = workouts.Take(3).ToList();
        }
    }
}
