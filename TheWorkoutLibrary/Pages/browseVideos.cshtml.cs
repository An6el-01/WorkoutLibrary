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

        [BindProperty] public string Search { get; set; }
        [BindProperty] public string Upload { get; set; }

        public IList<Excercise> excercises { get; private set; }

        private readonly UserManager<ApplicationUser> _um;

        public browseVideosModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }
        public void OnGet()
        {
            excercises = _db.Excercise.ToList();                
        }

        public IActionResult OnPostUpload()
        {
            return RedirectToPage("/Upload");

        }
        public IActionResult OnPostSearch()
        {
            excercises = _db.Excercise.Where(x => x.Name.Contains(Search)).ToList();          
            return Page();
        }
    }
}