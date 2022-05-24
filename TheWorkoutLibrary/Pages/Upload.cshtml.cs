using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TheWorkoutLibrary.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TheWorkoutLibrary.Pages
{
    public class UploadModel : PageModel
    {
        private AppDbContext _db;

        public string[] difficulties = new[] { "Beginner", "Intermediate", "Advanced" };

        [BindProperty]
        public Excercise excercise { get; set; }

        [BindProperty]
        public Workout Workout { get; set; }

        private readonly IHostingEnvironment _he;

        [BindProperty]
        public IFormFile Video { get; set; }

        public IList<Workout> workoutTbl { get; private set; }

        [BindProperty] public string newEmbed { get; set; }

        public UploadModel(AppDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            excercise.Name = Convert.ToString(Request.Form["name"]);
            excercise.Notes = Convert.ToString(Request.Form["notes"]);
            excercise.YoutubeURL = convertURL(Convert.ToString(Request.Form["youtube"]));
            excercise.Visibility = true;            
            _db.Excercise.Add(excercise);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Excercises");
        }
        public string convertURL(string oldString)
        {
            //All Client's Videos are Shorts on private Youtube 

            var res = oldString.Split("shorts/");
            newEmbed = "https://www.youtube.com/embed/" + res[1];
            return newEmbed;
        }

        
    }
}
