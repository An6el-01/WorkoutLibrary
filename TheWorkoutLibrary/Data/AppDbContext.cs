using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace TheWorkoutLibrary.Data
    
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserProfile> userProfiles { get; set; }
        public DbSet<Excercise> Excercise { get; set; }
        public DbSet<WorkoutExcercise> WorkoutExcercise { get; set; }
        public DbSet<Workout> Workout { get; set; }


        [NotMapped]
        public DbSet<WorkoutItem> WorkoutItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<BasketItem>().HasKey(t => new { t.workoutID, t.basketID });            
        }
    }   


}
