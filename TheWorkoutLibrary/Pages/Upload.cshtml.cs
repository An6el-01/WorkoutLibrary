using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWorkoutLibrary.Data;

namespace TheWorkoutLibrary.Pages
{
    public class UploadModel : PageModel
    {
       private AppDbContext _db;
       public Workout Workout { get; set; }
       public UploadModel(AppDbContext db)
        {
            _db = db;
        }

    }
}
