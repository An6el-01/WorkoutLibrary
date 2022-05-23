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
    public class createdWorkoutLibraryModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _um;

        [BindProperty] public string Search { get; set; }

        public IList<Workout> workouts { get; private set; }

        public IList<WorkoutExcercise> workoutExcercises { get; private set; }


        public createdWorkoutLibraryModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }


        public void OnGet()
        {
            workouts = _db.Workout
                .Include(workout => workout.Excercises)
                .ThenInclude(workoutExercise => workoutExercise.Excercise)
                .ToList();

            /*Items = _db.WorkoutItems.FromSqlRaw(
            "SELECT Excercise.Id, Excercise.Name, Excercise.YoutubeURL, WorkoutExcercise.Sets, WorkoutExcercise.Reps " +
            "FROM Excercise INNER JOIN WorkoutExcercise ON Excercise.Id = WorkoutExcercise.ExcerciseId ").ToList();*/

        }

        
    }
}

