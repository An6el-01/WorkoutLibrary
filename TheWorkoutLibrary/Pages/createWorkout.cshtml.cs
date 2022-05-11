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

namespace TheWorkoutLibrary.Pages
{
    [Authorize]
    public class createWorkoutModel : PageModel
    {
        private AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public IList<WorkoutItem> Items { get; set;}

        public createWorkoutModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _userManager = um;
        }
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutUser checkoutUser = await _db.CheckoutUsers.FindAsync(user.Email);
            Items = _db.WorkoutItems.FromSqlRaw("SELECT videoWorkoutLibrary.workoutID, videoWorkoutLibrary.workoutName,videoWorkoutLibrary.videoData, BasketItems.quantity  " +
                "FROM videoWorkoutLibrary INNER JOIN BasketItems ON videoWorkoutLibrary.workoutID = BasketItems.workoutID " +
                "WHERE basketID = {0}", checkoutUser.basketID).ToList();
        }
    }
}
