using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorkoutLibrary.Data
{
    public class WorkoutItem
    {
        [Key, Required]
        public int workoutID { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public string workoutName { get; set; }
        //[Required]
        //public int sets { get; set; }
        //[Required]
        //public int reps { get; set; }
        [Required]
        public byte[] videoData { get; set; }
    }
}
