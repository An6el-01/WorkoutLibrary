using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWorkoutLibrary.Data;




namespace TheWorkoutLibrary.Pages
{
    public class browseVideosModel : PageModel
    {
        private readonly AppDbContext _db;
        public IList<Workout> Workout { get; private set; }
        
        [BindProperty] public string Search { get; set; }
        [BindProperty] public string Upload { get; set; }

        public browseVideosModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Workout = _db.videoWorkoutLibrary.FromSqlRaw("SELECT * FROM videoWorkoutLibrary").ToList();
        }

        public IActionResult OnPostUpload()
        {
            return RedirectToPage("/Upload");

        }
        public IActionResult OnPostSearch()
        {
            /*Workout = _db.videoWorkoutLibrary.FromSqlRaw("SELECT * FROM videoWorkoutLibrary WHERE activityStatus =1 AND workoutName LIKE'" + Search + "%'").ToList();
            return Page();*/
            
            Workout = _db.videoWorkoutLibrary.FromSqlRaw("SELECT * FROM videoWorkoutLibrary WHERE workoutName LIKE '" + Search + "%'AND activityStatus = 1").ToList();
            return Page();
        }
    }
}
