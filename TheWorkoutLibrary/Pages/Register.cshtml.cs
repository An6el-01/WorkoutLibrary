using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWorkoutLibrary.Data;

namespace TheWorkoutLibrary.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegistrationModel Input { get; set; }
        private AppDbContext _db;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userInManager;

        public CheckoutUser CheckoutUser = new CheckoutUser();
        public Basket Basket = new Basket();

        public RegisterModel(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm, AppDbContext db )
        {
            _signInManager = sm;
            _userInManager = um;
            _db = db;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userInManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userInManager.AddToRoleAsync(user, "Member");
                    NewBasket();
                    NewCheckoutUser(Input.Email);
                    await _db.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
        public void NewBasket()
        {
            var currentBasket = _db.Baskets.FromSqlRaw("SELECT * FROM Baskets")
                .OrderByDescending(b => b.basketID)
                .FirstOrDefault();
            if (currentBasket == null)
            {
                Basket.basketID = 1;
            }
            else
            {
                Basket.basketID = currentBasket.basketID + 1;
            }
            _db.Baskets.Add(Basket);
        }
        public void NewCheckoutUser(string Email)
        {
            CheckoutUser.email = Email;            
            CheckoutUser.basketID = Basket.basketID;
            _db.CheckoutUsers.Add(CheckoutUser);
        }
    }
}
