using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWorkoutLibrary.Data;


namespace TheWorkoutLibrary.Pages
{
    public class ProfileModel : PageModel
    {
        private AppDbContext _db;
       
        [BindProperty] public UserProfile Image { get; set; }
        [BindProperty] public string Search { get; set; }
        [BindProperty] public string Upload { get; set; }
        
        

        public UserProfile currentUser { get; set; }


        private readonly UserManager<ApplicationUser> _um;

        public ProfileModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _um.GetUserAsync(User);
            currentUser = await _db.userProfiles.FindAsync(user.Email);
            if (currentUser == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        
        public async Task<IActionResult> OnPostImage()
        {
            
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    currentUser.ImageData = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                }

                _db.Attach(Image).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException e)
                {
                    throw new Exception($"not found", e);
                }
                
                return RedirectToPage("/Profile");
            }

        }
        
        public async Task<IActionResult> OnPostDeleteAsync(string Email)
        {
            var item = await _db.userProfiles.FindAsync(Email);
            item.SubscriptionStatus = false;
            _db.Attach(item).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Item {item.UserId} not found!", e);
            }
            
            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnPostRenewAsync(string Email)
        {
            var item = await _db.userProfiles.FindAsync(Email);
            item.SubscriptionStatus = true;
            _db.Attach(item).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Item {item.UserId} not found!", e);
            }
            
            return RedirectToPage();
        }
    }
    
}
    