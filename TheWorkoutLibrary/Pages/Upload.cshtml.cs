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

        [BindProperty]
        public Workout Workout { get; set; }

        private readonly IHostingEnvironment _he;

        [BindProperty]
        public IFormFile Video { get; set; }

        public IList<Workout> workoutTbl { get; private set; }

        public UploadModel(AppDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            Workout.activityStatus = true;
            foreach(var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Workout.videoData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            _db.videoWorkoutLibrary.Add(Workout);
            await _db.SaveChangesAsync();
            return RedirectToPage("/browseVideos");
        }

    }
}
