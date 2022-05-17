using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using TheWorkoutLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace TheWorkoutLibrary.Pages
{
    [Authorize]
    public class createWorkoutModel : PageModel
    {
        private AppDbContext _db;
        private readonly UserManager<ApplicationUser> _um;

        public string[] difficulties = new[] { "Beginner", "Intermediate", "Advanced" };

        public IList<WorkoutItem> Items { get; set;}
        public IList<WorkoutExcercise> workoutExcercises { get; set; }
        public IList<Workout> workouts { get; set; }
        public IList<Excercise> excercises { get; set; }

        [BindProperty]
        public Workout newWorkout { get; set; }

        [BindProperty]
        public Workout lastWorkout { get; set; }


        public createWorkoutModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }
        //public void OnGet()
        //{
        //    workouts = _db.Workout.ToList();
        //    excercises = _db.Excercise.ToList();
        //}
        public async Task OnGetAsync()
        {
            excercises = _db.Excercise.ToList();
            var user = await _um.GetUserAsync(User);
            UserProfile currentUser = await _db.userProfiles.FindAsync(user.Email);
                        
            workouts = _db.Workout.Where(x => x.UserId == (currentUser.UserId)).ToList();           
           
            lastWorkout = workouts.LastOrDefault();


            Items = _db.WorkoutItems.FromSqlRaw(
            "SELECT Excercise.Id, Excercise.Name, Excercise.YoutubeURL, WorkoutExcercise.Sets, WorkoutExcercise.Reps " + 
            "FROM Excercise INNER JOIN WorkoutExcercise ON Excercise.Id = WorkoutExcercise.ExcerciseId " +
            "Where WorkoutId = {0}", lastWorkout.Id).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {    
            if (!ModelState.IsValid) { return Page(); }
            var user = await _um.GetUserAsync(User);
            UserProfile currentUser = await _db.userProfiles.FindAsync(user.Email);

            var current = _db.Workout.FromSqlRaw("SELECT * FROM Workout")
                     .OrderByDescending(b => b.Id)
                     .FirstOrDefault();
            if (current == null)
            {
                Workout newWorkout = new Workout
                {
                    Id = 1,
                    Name = Convert.ToString(Request.Form["name"]),
                    Notes = Convert.ToString(Request.Form["notes"]),
                    UserId = currentUser.UserId,
                    ImageURL = Convert.ToString(Request.Form["image"])                    
            };

                _db.Workout.Add(newWorkout);
                await _db.SaveChangesAsync();
            }
            else
            {
                newWorkout.Id= current.Id + 1;
            }
            newWorkout.Name = Convert.ToString(Request.Form["name"]);
            newWorkout.Notes = Convert.ToString(Request.Form["notes"]);
            newWorkout.UserId = currentUser.UserId;
            newWorkout.Difficulty = Convert.ToString(Request.Form["difficulty"]);
            newWorkout.ImageURL = Convert.ToString(Request.Form["image"]);
            _db.Workout.Add(newWorkout);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Unable to create New Workout", e);
            }
            return Page();
        }
    }
}
