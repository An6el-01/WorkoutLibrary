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
        private readonly AppDbContext _db;
        /*public IList<CheckoutUser> Image { get; private set; }*/
        [BindProperty] public CheckoutUser Image { get; set; }
        [BindProperty] public string Search { get; set; }
        [BindProperty] public string Upload { get; set; }
        public CheckoutUser checkoutUser { get; set; }


        private readonly UserManager<ApplicationUser> _um;

        public ProfileModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _um = um;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _um.GetUserAsync(User);
            checkoutUser = await _db.CheckoutUsers.FindAsync(user.Email);
            if (checkoutUser == null)
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
                    checkoutUser.imageData = ms.ToArray();
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
    }
}
    