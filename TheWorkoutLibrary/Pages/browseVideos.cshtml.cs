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


        private readonly UserManager<ApplicationUser> _um;

        public browseVideosModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
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
            Workout = _db.videoWorkoutLibrary.FromSqlRaw("SELECT * FROM videoWorkoutLibrary WHERE workoutName LIKE '" + Search + "%'AND activityStatus = 1").ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAddAsync(int itemID)
        {
            var user = await _um.GetUserAsync(User);
            CheckoutUser checkoutUser = await _db.CheckoutUsers.FindAsync(user.Email);
            var item = _db.BasketItems.FromSqlRaw("SELECT * FROM BasketItems WHERE basketID = {0} AND workoutID ={1}", itemID, checkoutUser.basketID)
                .ToList().FirstOrDefault();
            if (item == null)
            {
                BasketItem newItem = new BasketItem
                {
                    basketID = checkoutUser.basketID,
                    workoutID = itemID,
                    quantity = 1
                };
                _db.BasketItems.Add(newItem);
                await _db.SaveChangesAsync();
            }
            else
            {
                item.quantity += 1;
                _db.Attach(item).State = EntityState.Modified;
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new Exception($"Unable to add items to createWorkout", e);
                }
            }
            return RedirectToPage("/createWorkout");
        }
    }
}
