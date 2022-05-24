using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using TheWorkoutLibrary.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace TheWorkoutLibrary.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditExcerciseModel : PageModel
    {


        [BindProperty]
        public Excercise excercise { get; set; }
        private AppDbContext _db;
        private readonly IHostingEnvironment _he;

        public string[] difficulties = new[] { "Beginner", "Intermediate", "Advanced" };

        public EditExcerciseModel(AppDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            excercise = await _db.Excercise.FindAsync(id);
            if (excercise == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            excercise.Name = Convert.ToString(Request.Form["name"]);
            excercise.Notes = Convert.ToString(Request.Form["notes"]);
            excercise.YoutubeURL = Convert.ToString(Request.Form["youtube"]);
            excercise.Visibility = true;
            _db.Attach(excercise).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Recipe{excercise.Id} not found", e);
            }
            
            return RedirectToPage("/Excercises");
        }
    }
}
