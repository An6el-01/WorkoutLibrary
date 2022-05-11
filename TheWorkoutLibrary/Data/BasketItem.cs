using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TheWorkoutLibrary.Data
{
    public class BasketItem
    {
        [Required]
        public int workoutID { get; set; }
        [Required]
        public int basketID { get; set; }
        [Required]
        public int quantity { get; set; }
    }
}
