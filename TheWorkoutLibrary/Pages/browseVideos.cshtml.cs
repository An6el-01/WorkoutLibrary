using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        

        public browseVideosModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Workout = _db.videoWorkoutLibrary.FromSqlRaw("SELECT * FROM videoWorkoutLibrary").ToList();
        }
    }
}
