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

        
        public IList<WorkoutExcercise> workoutExcercises { get; set; }
        public IList<Workout> workouts { get; set; }
        public IList<Excercise> excercises { get; set; }

        [BindProperty]
        public WorkoutExcercise newWorkoutExcercise { get; set; }

        [BindProperty]
        public Workout newWorkout { get; set; }

        [BindProperty]
        public Workout lastWorkout { get; set; }


        public createWorkoutModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }

        public async Task OnGetAsync()
        {
            workoutExcercises = _db.WorkoutExcercise.ToList();            

            excercises = _db.Excercise.ToList();

            var user = await _um.GetUserAsync(User);
            UserProfile currentUser = await _db.userProfiles.FindAsync(user.Email);
                        
            workouts = _db.Workout
                .Where(x => x.UserId == (currentUser.UserId))
                .ToList();           
           
            lastWorkout = workouts.LastOrDefault();

            // Need to add if statement if value equal NULL for new users who havent created workout yet
            if (lastWorkout == null)
            {
                workoutExcercises = _db.WorkoutExcercise
               .Where(x => x.WorkoutId == 0)
               .ToList();
            }
            else
            {
                workoutExcercises = _db.WorkoutExcercise
               .Where(x => x.WorkoutId == lastWorkout.Id)
               .ToList();
            }
           
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

            excercises = _db.Excercise.ToList();

            workouts = _db.Workout
                .Where(x => x.UserId == (currentUser.UserId))
                .ToList();

            workoutExcercises = _db.WorkoutExcercise
                .Where(x => x.WorkoutId == lastWorkout.Id)
                .ToList();
            lastWorkout = workouts.LastOrDefault();

            return Page();
        }
        public async Task<IActionResult> OnPostAddAsync(int ExcerciseId)
        {
            var user = await _um.GetUserAsync(User);
            UserProfile currentUser = await _db.userProfiles.FindAsync(user.Email);
            var item = await _db.Excercise.FindAsync(ExcerciseId);

            workouts = _db.Workout
                .Where(x => x.UserId == (currentUser.UserId))
                .ToList();

            lastWorkout = workouts.LastOrDefault();

            workoutExcercises = _db.WorkoutExcercise.ToList();
            var current = workoutExcercises.LastOrDefault();                      

            if (current.Id < 1)
            {
                WorkoutExcercise newWorkoutExc = new WorkoutExcercise
                {
                    Id = 1,
                    ExcerciseId = ExcerciseId,
                    WorkoutId = lastWorkout.Id,
                    Sets = Convert.ToInt32(Request.Form["sets"]),
                    Reps = Convert.ToInt32(Request.Form["reps"]),
                    Rest = Convert.ToInt32(Request.Form["rest"])
                };

                _db.WorkoutExcercise.Add(newWorkoutExc);
                await _db.SaveChangesAsync();
            }
            else
            {
                newWorkoutExcercise.Id = current.Id + 1;
            }
            newWorkoutExcercise.ExcerciseId = ExcerciseId;
            newWorkoutExcercise.WorkoutId = lastWorkout.Id;           
            newWorkoutExcercise.Sets = Convert.ToInt32(Request.Form["sets"]);
            newWorkoutExcercise.Reps = Convert.ToInt32(Request.Form["reps"]);
            newWorkoutExcercise.Rest = Convert.ToInt32(Request.Form["rest"]);
            _db.WorkoutExcercise.Add(newWorkoutExcercise);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Unable to create New Workout", e);
            }
            workoutExcercises = _db.WorkoutExcercise
              .Where(x => x.WorkoutId == lastWorkout.Id)
              .ToList();

            return RedirectToPage();
        }
    }
}
